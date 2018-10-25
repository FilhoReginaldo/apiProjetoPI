using System.ComponentModel.DataAnnotations;

namespace Efficacy.Api.Models
{
    public class FamiliaCerveja
    {
        public int ID{get;set;}
        [StringLength(100)]
        public string Nome{get;set;}
        [StringLength(100)]
        public string Descricao{get;set;}
    }
}