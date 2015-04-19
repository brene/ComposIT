using ComposIT.Model.ClassStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComposIT.Model.Translation
{
    /// <summary>
    /// A C# Translator which translates a ClassObject into fitting "c# code".
    /// </summary>
    class CSharpTranslation : TranslationStrategy
    {
        /// <summary>
        /// Translates a specific ClassObject.
        /// </summary>
        /// <param name="cObject"></param>
        /// <returns></returns>
        public override StringBuilder TranslateClass(ClassObject cObject)
        {
            if (cObject.GetType() == typeof(MainClassObject))
            {
                result.Append(@"\cf5------------------MainClass.cs------------------\line\line");
                TranslationHeader();

                TranslateMainClass(cObject);

                TranslationFooter();
            }
            else
            {
                result.Append(@"\cf5------------------" + CapitalFirst(cObject.ClassName) + @".cs------------------\line\line");
                TranslationHeader();

                result.Append(@"\tab\cf9public class \cf10" + cObject.ClassName + @"\cf1\line");
                result.Append(@"\tab\{");

                if (cObject.PrimitiveAttributes.Count > 0)
                {
                    result.Append(@"\line");
                }
                TranslatePrimitiveAttributes(cObject);

                if (cObject.Attributes.Count > 0)
                {
                    result.Append(@"\line");
                }
                TranslateObjectAttributes(cObject);

                if (cObject.Methods.Count > 0)
                {
                    result.Append(@"\line");
                }
                TranslateMethods(cObject);

                if (cObject.Methods.Count + cObject.Attributes.Count + cObject.PrimitiveAttributes.Count == 0)
                {
                    result.Append(@"\line");
                }

                result.Append(@"\tab\}\line");
                TranslationFooter();
            }

            return result;
        }

        /// <summary>
        /// Translates the ClassObject of the type MainClass which also has a main method.
        /// </summary>
        /// <param name="cObject">The object that is translated.</param>
        private void TranslateMainClass(ClassObject cObject)
        {
            result.Append(@"\tab\cf9public class \cf10 MainClass \cf1\line");
            result.Append(@"\tab\{\line");
            result.Append(@"\tab\tab\cf9public static void \cf1 Main()\line");
            result.Append(@"\tab\tab\{\line");
            foreach (InstanceObject subObject in cObject.Attributes)
            {
                string classString = subObject.ClassObject.ClassName;
                string attrString = subObject.AttributeName ?? classString.ToLowerInvariant();

                result.Append(@"\tab\tab\tab\cf10" + classString + @" \cf1" + attrString + 
                    @" = \cf9new \cf10" + classString + @"\cf1();\line");
            }
            if (cObject.Attributes.Count == 0)
            {
                result.Append(@"\line");
            }
            result.Append(@"\tab\tab\}\line");
            result.Append(@"\tab\}\line");
        }

        /// <summary>
        /// Translates the more complex Object Attributes. 
        /// </summary>
        /// <param name="cObject"></param>
        private void TranslateObjectAttributes(ClassObject cObject)
        {
            HashSet<string> includedSet = new HashSet<string>();
            foreach (var attribute in cObject.Attributes)
            {
                // Translate each type only once
                if (includedSet.Contains(attribute.ClassObject.Id))
                {
                    continue;
                }
                includedSet.Add(attribute.ClassObject.Id);

                // Set array
                if (NumberOfInstances[attribute.ClassObject.Id] > 1)
                {
                    string classString = attribute.ClassObject.ClassName;
                    string attrString = CapitalFirst(attribute.AttributeName ?? classString.ToLowerInvariant() + "s");

                    result.Append(@"\tab\tab\cf9public \cf10" + classString + @"\cf1[] " + attrString + @" = \{ ");
                    for (int i = 0; i < NumberOfInstances[attribute.ClassObject.Id]; i++)
                    {
                        if (i != 0)
                        {
                            result.Append(", ");
                        }
                        result.Append(@"new \cf10" + classString + @"\cf1()");
                    }
                    result.Append(@" \};\line");
                }
                // Or single attribute 
                else
                {
                    string classString = attribute.ClassObject.ClassName;
                    string attrString = CapitalFirst(attribute.AttributeName ?? classString);
                    result.Append(@"\tab\tab\cf9public \cf10" + classString + @" \cf1" + attrString + @" \{ \cf9get\cf1; \cf9set\cf1; \};\line");
                }
            }
        }

        /// <summary>
        /// Translates all the primitive attributes of the cObject in c# code.
        /// </summary>
        /// <param name="cObject"></param>
        private void TranslatePrimitiveAttributes(ClassObject cObject)
        {
            foreach (string attributeName in cObject.PrimitiveAttributes.Keys.ToList<string>())
            {
                string type;
                switch (cObject.PrimitiveAttributes[attributeName])
                {
                    case PrimitiveTypes.Boolean:
                        type = "bool";
                        break;
                    case PrimitiveTypes.Float:
                        type = "float";
                        break;
                    case PrimitiveTypes.Integer:
                        type = "int";
                        break;
                    case PrimitiveTypes.String:
                        type = "string";
                        break;
                    default:
                        type = @"\cf10Type";
                        break;
                }
                result.Append(@"\tab\tab\cf9public " + type + @" \cf1" + CapitalFirst(attributeName) + @" \{ \cf9get\cf1; \cf9set\cf1; \}\line");
            }
        }

        /// <summary>
        /// Translates the methods of the cobject into c# code.
        /// </summary>
        /// <param name="cObject"></param>
        private void TranslateMethods(ClassObject cObject)
        {
            foreach (string methodName in cObject.Methods)
            {
                result.Append(@"\tab\tab\cf9public void \cf1" + CapitalFirst(methodName) + @"()\line");
                result.Append(@"\tab\tab\{\line");
                result.Append(@"\line");
                result.Append(@"\tab\tab\}\line");
            }
        }

        /// <summary>
        /// The name of this Language.
        /// </summary>
        /// <returns>The name of the language.</returns>
        public override string GetLanguageName()
        {
            return "C#";
        }

        /// <summary>
        /// Sets a header for the namespace of the class.
        /// </summary>
        private void TranslationHeader()
        {
            result.Append(@"\cf9 using \cf1 System;\line");
            result.Append(@"\cf9 namespace \cf1 HelloWorld\line");
            result.Append(@"\{\line");
        }

        /// <summary>
        /// closes the namespace.
        /// </summary>
        private void TranslationFooter()
        {
            result.Append(@"\}\line\line");
        }
    }
}
