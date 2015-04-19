using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComposIT.Model.ClassStructure;

namespace ComposIT.Model.Translation
{
    public abstract class TranslationStrategy
    {

        /// <summary>
        /// the list of the ClassObjects which are already written.
        /// </summary>
        public List<ClassObject> completedList;

        /// <summary>
        /// the list of the ClassObjects which are not written yet.
        /// </summary>
        public List<ClassObject> todoList;

        /// <summary>
        /// the String representation of all the classes.
        /// </summary>
        public StringBuilder result;

        /// <summary>
        /// Saves the name to an Id.
        /// </summary>
        public Dictionary<string, int> NumberOfInstances;

        /// <summary>
        /// A Startegy for the programming language translation.
        /// </summary>
        public TranslationStrategy()
        {
            result = new StringBuilder();
            completedList = new List<ClassObject>();
            todoList = new List<ClassObject>();
            NumberOfInstances = new Dictionary<string, int>();
        }

        /// <summary>
        /// translates all the classes which are included (directly or indirectly) in the root.
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        public StringBuilder Translate(ClassObject root)
        {
            result.Clear();
            completedList.Clear();
            todoList.Clear();

            result.Append(@"{\rtf1\ansi\deff0");

            //color definitions
            result.Append(@" {\colortbl;");
            result.Append(@"\red255\green255\blue255;"); //white cf1
            result.Append(@"\red234\green128\blue252;"); //allen purple cf2
            result.Append(@"\red29\green233\blue182;"); //turqouise cf3
            result.Append(@"\red255\green183\blue77;"); //orange cf4
            result.Append(@"\red84\green110\blue122;"); //allen gray cf5
            result.Append(@"\red174\green213\blue129;"); //allen green cf6
            result.Append(@"\red128\green216\blue255;"); //allen blue cf7
            result.Append(@"\red255\green213\blue79;"); //allen orange cf8
            result.Append(@"\red86\green156\blue214;"); //dome blue cf9
            result.Append(@"\red78\green201\blue176;"); //dome green cf10
            result.Append(@"\red33\green150\blue243;"); //allen swift blue cf11
            result.Append(@"\red255\green152\blue0;"); //allen swift orange cf12
            result.Append(@"\red189\green189\blue189;"); //allen swift gray cf13
            

            result.Append("}");

            todoList.Add(root);
            todoList.AddRange(ModelManager.GetInstance().GetClasses());

            while (todoList.Count > 0)
            {
                NumberOfInstances.Clear();
                ClassObject cObject = todoList.First();
                foreach (var subObject in cObject.Attributes)
                {
                    if (!NumberOfInstances.ContainsKey(subObject.ClassObject.Id)) 
                    {
                        NumberOfInstances.Add(subObject.ClassObject.Id, 1);
                    }
                    else
                    {
                        NumberOfInstances[subObject.ClassObject.Id]++;
                    }
                }
                todoList.Remove(cObject);
                completedList.Add(cObject);
                TranslateClass(cObject);
                foreach (var attribute in cObject.Attributes)
                {
                    if(!completedList.Contains(attribute.ClassObject) && !(todoList.Contains(attribute.ClassObject))) {
                        todoList.Add(attribute.ClassObject);    // Should never be called (contained for compatibility and safety)
                    }
                }
            }

            result.Append("}");
            return result;
        }


        /// <summary>
        /// Translates a specific ClassObject.
        /// </summary>
        /// <param name="cObject"></param>
        /// <returns></returns>
        public abstract StringBuilder TranslateClass(ClassObject cObject);

        /// <summary>
        /// returns the name of the strategy
        /// </summary>
        /// <returns></returns>
        public abstract String GetLanguageName();

        /// <summary>
        /// makes the first latter capital.
        /// </summary>
        /// <param name="input">the string whos first letter is made captial</param>
        /// <returns>The first char captialized word.</returns>
        protected string CapitalFirst(string input)
        {
            return Char.ToUpper(input[0]) + input.Substring(1);
        }

        /// <summary>
        /// Makes first char of string to lower case.
        /// </summary>
        /// <param name="input">The string to make first letter lowercse of.</param>
        /// <returns>string with lower case.</returns>
        protected string LowerFirst(string input)
        {
            return Char.ToLower(input[0]) + input.Substring(1);
        }
    }
}
