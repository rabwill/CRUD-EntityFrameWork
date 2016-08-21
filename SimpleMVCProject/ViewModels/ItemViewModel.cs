using SimpleMVCProject.Models;
using System.Runtime.Serialization;

using Utils.Web;

namespace SimpleMVCProject.ViewModels
{
    [DataContract]
    public class ItemViewModel : BaseValidatableViewModel<ItemViewModel, Item>
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int Place { get; set; }

        [DataMember]
        public string Username { get; set; }

        [DataMember]
        public string DateOfTravel { get; set; }

        [DataMember]
        public string FlightName { get; set; }

        [DataMember]
        public string FlightCost { get; set; }

        [DataMember]
        public string Website { get; set; }

      
    }
}