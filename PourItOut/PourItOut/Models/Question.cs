using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PourItOut.Models
{
    public class Question
    {
        int id;
        string text;

        public int Id { get => id; set => id = value; }
        public string Text { get => text; set => text = value; }

        public override bool Equals(object obj)
        {
            return obj is Question question &&
                   Id == question.Id &&
                   Text == question.Text;
        }

        public override string ToString()
        {
            return text;
        }
    }
}
