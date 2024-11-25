using DemoProject.EventSystem;
using UnityEngine;

namespace DemoProject.LevelManagement.Events
{
    [CreateAssetMenu(fileName = "New Competitor Event", menuName = "EventSystem/Events/Competitor Event")]
    public class CompetitorEvent : GameEventBase<CompetitorBase> {}
}