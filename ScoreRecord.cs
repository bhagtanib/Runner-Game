
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RunnerGame
{
    [Table("score", Schema = "CS371GroupC")]
    internal class DbScore
    {
        public int id { get; set; }
        public int? score { get; set; }


        //[ForeignKey("group_id")]
        //public DbGroup? groups { get; set; }
    }
}
