using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
namespace PM2E12513.Models1
{
    public class Users
    {
        [PrimaryKey, AutoIncrement]
        public int codigo { get; set; }

        [MaxLength(70)]

        public string nombre { get; set; }

        [MaxLength(250)]
        public string constrasenia { get; set; }

    }
}
