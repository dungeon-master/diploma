using System;
using System.Collections.Generic;
using ECatalogRecommendations.DbEntities;
using ECatalogRecommendations.Models;

namespace ECatalogRecommendations.Analyzers
{
    public class ActionAnalyzer : IActionAnalyzer
    {   
        private enum State
        {
            StartState,
            SearchResult,
            Description,
            Requirement,
            Order,
            DigitalResources
        }

        private enum Transition
        {
            InvokeSimpleSearch = 0,
            InvokeAdvancedSearch = 1,
            ViewDescription = 3,
            PrintRequirement = 4,
            AttemptOrder = 5,
            ViewDigitalResources = 6
        }

        private class StateTransition
        {
            private readonly State _currentState;
            private readonly Transition _transition;

            public StateTransition(State currentState, Transition transition)
            {
                _currentState = currentState;
                _transition = transition;
            }

            public override int GetHashCode()
            {
                return _currentState.GetHashCode() + _transition.GetHashCode();
            }

            public override bool Equals(object obj)
            {
                bool result = false;
                StateTransition newObj = obj as StateTransition;
                if (newObj != null && _currentState == newObj._currentState && _transition == newObj._transition)
                {
                    result = true;
                }
                return result;
            }
        }

        private const double CoefViewDescription = 1.5;
        private const double CoefRetrySearch = 0.6;
        private const double CoefNewSearch = 0.5;
        private const double CoefSearchSuccesfull = 1.0;
        private const double MaxWeight = 0.999;

        private readonly Dictionary<StateTransition, State> _transitions;
        private State _currentState;
        private string _currentSession;
        private SearchRequest _lastSearchRequest;
        private SearchRequest _currentSearchRequest;
        private double _usefullness;

        private readonly List<Tuple<SearchRequest, double>> _result = new List<Tuple<SearchRequest, double>>(); 

        public ActionAnalyzer()
        {
            _currentState = State.StartState;
            _lastSearchRequest = null;
            _currentSession = null;
            _currentSearchRequest = null;
            _usefullness = 0;
            _transitions = new Dictionary<StateTransition, State>
            {
                { new StateTransition(State.StartState, Transition.InvokeSimpleSearch), State.SearchResult },
                { new StateTransition(State.StartState, Transition.InvokeAdvancedSearch), State.SearchResult },
                { new StateTransition(State.SearchResult, Transition.InvokeSimpleSearch), State.SearchResult },
                { new StateTransition(State.SearchResult, Transition.InvokeAdvancedSearch), State.SearchResult },
                { new StateTransition(State.SearchResult, Transition.ViewDescription), State.Description },
                { new StateTransition(State.SearchResult, Transition.AttemptOrder), State.Order },
                { new StateTransition(State.SearchResult, Transition.PrintRequirement), State.Requirement },
                { new StateTransition(State.SearchResult, Transition.ViewDigitalResources), State.DigitalResources },
                { new StateTransition(State.Description, Transition.InvokeSimpleSearch), State.SearchResult },
                { new StateTransition(State.Description, Transition.InvokeAdvancedSearch), State.SearchResult },
                { new StateTransition(State.Description, Transition.AttemptOrder), State.Order },
                { new StateTransition(State.Description, Transition.ViewDescription), State.Description },
                { new StateTransition(State.Description, Transition.PrintRequirement), State.Requirement },
                { new StateTransition(State.Description, Transition.ViewDigitalResources), State.DigitalResources }
            };
        }

        public void AnalyzeAction(FrontOfficeAction action)
        {
            if (_currentSession != null && _currentSession != action.FrontOfficeSessionId)
            {
                FinishSequenceAnalyze();
            }
            _currentSession = action.FrontOfficeSessionId;
            int? code = action.ActionCode;
            if (code != null && Enum.IsDefined(typeof(Transition), code))
            {
                Transition transition = (Transition)code;
                if (transition == Transition.InvokeSimpleSearch || transition == Transition.InvokeAdvancedSearch)
                {
                    _currentSearchRequest = new SearchRequest(action.ActionComment, action.FrontOfficeSessionId);
                    if (_lastSearchRequest == null)
                    {
                        _lastSearchRequest = new SearchRequest(_currentSearchRequest);
                    }
                }
                MoveNext(transition);
            }
        }

        public List<Tuple<SearchRequest, double>> GetResult()
        {
            return _result;
        }

        private void FinishSequenceAnalyze()
        {
            if (_usefullness > 0 && _lastSearchRequest != null && _lastSearchRequest.IsValid())
            {
                double weight = 1.0 - _usefullness;
                if (weight > MaxWeight)
                {
                    weight = MaxWeight;
                }
                _result.Add(new Tuple<SearchRequest, double>(_lastSearchRequest, weight));
            }
            StartAgain();
        }

        private void MoveNext(Transition transition)
        {
            StateTransition stateTransition = new StateTransition(_currentState, transition);
            State nextState;
            if (_transitions.TryGetValue(stateTransition, out nextState))
            {
                AnalyzeTransitionToState(nextState);
                _currentState = nextState;
                if (_usefullness.Equals(CoefSearchSuccesfull))
                {
                    StartAgain();
                }
            }
        }

        private void AnalyzeTransitionToState(State newState)
        {
            if (_currentState == State.StartState)
            {
                if (newState == State.SearchResult)
                {
                    _usefullness = CoefNewSearch;
                }
            }
            else if (newState == State.SearchResult)
            {
                if (NGramAnalyzer.IsSimilar(_lastSearchRequest, _currentSearchRequest))
                {
                    _usefullness *= CoefRetrySearch;
                    _lastSearchRequest = _currentSearchRequest;
                }
                else
                {
                    FinishSequenceAnalyze();
                    _usefullness = CoefNewSearch;
                    _lastSearchRequest = _currentSearchRequest;
                }
            }
            else if (newState == State.Description)
            {
                if (_currentState != State.Description)
                {
                    _usefullness *= CoefViewDescription;
                }
            }
            else
            {
                _usefullness = CoefSearchSuccesfull;
            }
        }

        private void StartAgain()
        {
            _lastSearchRequest = null;
            _usefullness = 0;
            _currentState = State.StartState;
        }
    }
}
