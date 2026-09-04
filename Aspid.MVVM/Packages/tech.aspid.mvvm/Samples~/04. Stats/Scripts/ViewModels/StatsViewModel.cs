using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace Aspid.MVVM.Samples.Stats
{
    // Edits a draft of the hero's skills. Nothing reaches the model until Confirm.
    [ViewModel]
    public sealed partial class StatsViewModel : IDisposable
    {
        [OneWayBind] private int _strength;
        [OneWayBind] private int _agility;
        [OneWayBind] private int _intelligence;
        [OneWayBind] private int _pointsAvailable;

        // True while the draft differs from the model. Drives CanExecute of Confirm and Reset.
        [OneWayBind] private bool _isDraft;

        private readonly Hero _hero;

        public StatsViewModel(Hero hero)
        {
            _hero = hero;
            _hero.Changed += Reset;

            Reset();
        }

        // Commands with a parameter: the button passes the Skill it belongs to.
        [RelayCommand(CanExecute = nameof(CanAdd))]
        private void Add(Skill skill)
        {
            Set(skill, Get(skill) + 1);
            PointsAvailable--;
        }

        private bool CanAdd(Skill skill) =>
            PointsAvailable > 0;

        [RelayCommand(CanExecute = nameof(CanRemove))]
        private void Remove(Skill skill)
        {
            Set(skill, Get(skill) - 1);
            PointsAvailable++;
        }

        private bool CanRemove(Skill skill) =>
            Get(skill) > _hero[skill];

        [RelayCommand(CanExecute = nameof(IsDraft))]
        private void Confirm()
        {
            var skills = new Dictionary<Skill, int>
            {
                { Skill.Strength, Strength },
                { Skill.Agility, Agility },
                { Skill.Intelligence, Intelligence },
            };

            // The model raises Changed, which calls Reset and clears the draft.
            _hero.Apply(skills, PointsAvailable);
        }

        [RelayCommand(CanExecute = nameof(IsDraft))]
        private void Reset()
        {
            Strength = _hero[Skill.Strength];
            Agility = _hero[Skill.Agility];
            Intelligence = _hero[Skill.Intelligence];
            PointsAvailable = _hero.PointsAvailable;

            // Set explicitly: after Confirm the budget may already equal the model's, so the hook below stays silent.
            IsDraft = false;
        }

        // Any change of the points budget means the draft moved away from, or back to, the model.
        partial void OnPointsAvailableChanged(int newValue)
        {
            IsDraft = newValue != _hero.PointsAvailable;

            AddCommand.NotifyCanExecuteChanged();
            RemoveCommand.NotifyCanExecuteChanged();
        }

        partial void OnIsDraftChanged(bool newValue)
        {
            ConfirmCommand.NotifyCanExecuteChanged();
            ResetCommand.NotifyCanExecuteChanged();
        }

        private int Get(Skill skill) => skill switch
        {
            Skill.Strength => Strength,
            Skill.Agility => Agility,
            Skill.Intelligence => Intelligence,
            _ => throw new ArgumentOutOfRangeException(nameof(skill), skill, null),
        };

        private void Set(Skill skill, int value)
        {
            switch (skill)
            {
                case Skill.Strength: Strength = value; break;
                case Skill.Agility: Agility = value; break;
                case Skill.Intelligence: Intelligence = value; break;
                default: throw new ArgumentOutOfRangeException(nameof(skill), skill, null);
            }
        }

        public void Dispose() =>
            _hero.Changed -= Reset;
    }
}
