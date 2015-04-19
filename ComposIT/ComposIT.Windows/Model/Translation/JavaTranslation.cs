using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ComposIT.Model.ClassStructure;

namespace ComposIT.Model.Translation
{
    /// <summary>
    /// Translates the class object into Java
    /// </summary>
    public class JavaTranslation : TranslationStrategy
    {

        /// <summary>
        /// Translates a specific ClassObject into Java.
        /// </summary>
        /// <param name="cObject"></param>
        /// <returns></returns>
        public override StringBuilder TranslateClass(ClassObject cObject)
        {
            result.Append(@"\cf9 ----------------------------" + cObject.ClassName + @".java----------------------------\line\line");
            if (cObject.GetType() == typeof(MainClassObject))
            {
                TranslateMainClass(cObject);

            }
            else
            {
                result.Append(@"\cf2\b public class \b0 \cf1" + cObject.ClassName + @" \{\line\line");

                TranslatePrimitiveAttributes(cObject);
                if (cObject.PrimitiveAttributes.Count > 0)
                {
                    result.Append(@"\line");
                }
                TranslateObjectAttributes(cObject);
                if (cObject.Attributes.Count > 0)
                {
                    result.Append(@"\line");
                }
                TranslateGetterAndSettersForPrimitiveAttributes(cObject);
                TranslateGetterAndSettersForObjectAttributes(cObject);
                TranslateMethods(cObject);

                result.Append(@"\}\line\line");
            }
            return result;
        }

        /// <summary>
        /// Translates a main class from cObject.
        /// </summary>
        /// <param name="cObject"></param>
        private void TranslateMainClass(ClassObject cObject)
        {
            result.Append(@"\cf2\b public class\b0 \cf1  MainClass \{\line\line");
            result.Append(@"\tab\cf2\b public static void \b0 \cf1 main(String[] args) \{\line");
            foreach (InstanceObject subObject in cObject.Attributes)
            {
                string classString = subObject.ClassObject.ClassName;
                string attrString = subObject.AttributeName ?? classString.ToLowerInvariant();

                result.Append(@"\tab\tab\cf2\b " + classString + @"\b0  \cf1" + attrString +
                    @" = \cf2\b new \b0 \cf1" + classString + @"();\line");
            }
            result.Append(@"\cf1\tab\}\line\line");
            result.Append(@"\cf1\}\line\line");
        }

        /// <summary>
        /// Translates all the Object Attributes of the cObject.
        /// </summary>
        /// <param name="cObject"></param>
        private void TranslateObjectAttributes(ClassObject cObject)
        {
            HashSet<string> includedSet = new HashSet<string>();
            foreach (var attribute in cObject.Attributes)
	        {
                if (includedSet.Contains(attribute.ClassObject.Id)) {
                    continue;
                }
                includedSet.Add(attribute.ClassObject.Id);

                if (NumberOfInstances[attribute.ClassObject.Id] > 1) {
                    result.Append(@"\tab " + attribute.ClassObject.ClassName + @"\cf1[] " + (attribute.AttributeName ?? attribute.ClassObject.ClassName.ToLowerInvariant() + "s")  + @" = \{ ");
                    for (int i = 0; i < NumberOfInstances[attribute.ClassObject.Id]; i++) {
                        result.Append(@"\b \cf2 new \b0  \cf1" + attribute.ClassObject.ClassName + "()");
                        if (i < NumberOfInstances[attribute.ClassObject.Id] - 1)
                        {
                            result.Append(", ");
                        }
                    }
                    result.Append(@" \};\line");
                }
                else
                {
                    if (attribute.AttributeName == null)
                    {
                        result.Append(@"\cf2 \tab \b " + attribute.ClassObject.ClassName + @" \b0 \cf1 " + attribute.ClassObject.ClassName.ToLowerInvariant() + @";\line");
                    }
                    else
                    {
                        result.Append(@"\cf2 \tab " + attribute.ClassObject.ClassName + @"\cf1 " + attribute.AttributeName + @";\line");
                    }
                }
	        }
        }

        /// <summary>
        /// Translates primitibe Attributes of cObject into Java.
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
                        type = "boolean";
                        break;
                    case PrimitiveTypes.Float:
                        type = "float";
                        break;
                    case PrimitiveTypes.Integer:
                        type = "int";
                        break;
                    case PrimitiveTypes.String:
                        type = "String";
                        break;
                    default:
                        type = "type";
                        break;
                }
                result.Append(@"\tab \cf2 \b private " + type + @"\b0 \cf1  " + attributeName + @";\line");
            }
        }

        /// <summary>
        /// Translates getters and setts for the primtive attributes in Java.
        /// </summary>
        /// <param name="cObject"></param>
        private void TranslateGetterAndSettersForPrimitiveAttributes(ClassObject cObject)
        {
            foreach (string attributeName in cObject.PrimitiveAttributes.Keys.ToList<string>())
            {
                string type;
                switch (cObject.PrimitiveAttributes[attributeName])
                {
                    case PrimitiveTypes.Boolean:
                        type = "boolean";
                        break;
                    case PrimitiveTypes.Float:
                        type = "float";
                        break;
                    case PrimitiveTypes.Integer:
                        type = "int";
                        break;
                    case PrimitiveTypes.String:
                        type = "String";
                        break;
                    default:
                        type = "type";
                        break;
                }

                //generate getter
                string toUpperCaseAttribute = char.ToUpper(attributeName[0]) + attributeName.Substring(1);
                result.Append(@"\tab\cf2 \b public " + type + @"\b0 \cf1  get" + toUpperCaseAttribute + @"() \{\line");
                result.Append(@"\tab\tab\cf2 \b return this\b0\cf1." + @"\cf1" + attributeName + @";\line");
                result.Append(@"\tab\}\line");
                result.Append(@"\line");

                //generate setter
                result.Append(@"\tab\cf2 \b public void\b0 \cf1  set" + toUpperCaseAttribute + @"(" + type + " " + attributeName + @") \{\line");
                result.Append(@"\tab \tab \cf2 \b this\b0 \cf1." + @"\cf1" + attributeName + @" = " + attributeName + @";\line");
                result.Append(@"\tab\}");
                result.Append(@"\line\line");
            }

        }

        /// <summary>
        /// Translates getters and setters for the more complex Object such as creatung arrays.
        /// </summary>
        /// <param name="cObject"></param>
        private void TranslateGetterAndSettersForObjectAttributes(ClassObject cObject) {
            HashSet<string> includedSet = new HashSet<string>();
            foreach (var attribute in cObject.Attributes)
            {
                if (includedSet.Contains(attribute.ClassObject.Id))
                {
                    continue;
                }
                includedSet.Add(attribute.ClassObject.Id);

                string type = "";
                string attributeName = "";
                if (NumberOfInstances[attribute.ClassObject.Id] > 1)
                {
                    type = (@"\cf1" + attribute.ClassObject.ClassName + @"[]");
                    if (attribute.AttributeName == null)
                    {
                        attributeName = attribute.ClassObject.ClassName.ToLowerInvariant() + "s";
                    }
                    else
                    {
                        attributeName = attribute.AttributeName;
                    }
                }
                else
                {
                    type = (attribute.ClassObject.ClassName);
                    if (attribute.AttributeName == null)
                    {
                        attributeName = attribute.ClassObject.ClassName.ToLowerInvariant();
                    }
                    else
                    {
                        attributeName = attribute.AttributeName;
                    }
                }

                //generate getter
                string toUpperCaseAttribute = char.ToUpper(attributeName[0]) + attributeName.Substring(1);
                result.Append(@"\tab\cf2 \b public\b0  " + type + @" \cf1 get" + toUpperCaseAttribute + @"() \{\line");
                result.Append(@"\tab\tab\cf2 \b return this\b0 \cf1." + @"\cf1" + attributeName + @";\line");
                result.Append(@"\tab\}\line");
                result.Append(@"\line");

                //generate setter
                result.Append(@"\tab\cf2 \b public void\b0 \cf1  set" + toUpperCaseAttribute + @"(" + type + " " + attributeName + @") \{\line");
                result.Append(@"\tab\tab\cf2 \b this\b0 \cf1." + @"\cf1" + attributeName + @" = " + attributeName + @";\line");
                result.Append(@"\tab\}");
                result.Append(@"\line\line");
            }
        }

        /// <summary>
        /// Translates all the defined methods.
        /// </summary>
        /// <param name="cObject"></param>
        private void TranslateMethods(ClassObject cObject) {
                foreach(string method in cObject.Methods) {
                    result.Append(@"\tab\cf2 \b public void \b0 \cf1 " + char.ToLower(method[0]) + method.Substring(1) + @"() \{\line");
                    result.Append(@"\tab\}");
                    result.Append(@"\line\line");
                }
        }

        /// <summary>
        /// Returns the Language Names.
        /// </summary>
        /// <returns></returns>
        public override string GetLanguageName()
        {
            return "Java";
        }
    }
}
