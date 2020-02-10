using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TodoListApi.Models
{
    public class Tache
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime DateCreation { get; set; }
        public DateTime DateEcheance { get; set; }
        public int Priorite { get; set; }
        public bool Terminee { get; set; }

        public Tache()
        {

        }
        public Tache(int id, string description, DateTime dateCreation, DateTime dateEcheance, int priorite, bool terminee)
        {
            Id = id;
            Description = description;
            DateCreation = dateCreation;
            DateEcheance = dateEcheance;
            Priorite = priorite;
            Terminee = terminee; 
        }
    }
}
