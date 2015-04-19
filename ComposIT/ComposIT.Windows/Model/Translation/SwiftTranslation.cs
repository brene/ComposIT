using ComposIT.Model.ClassStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComposIT.Model.Translation
{
    class SwiftTranslation : TranslationStrategy
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
            }
            else
            {
                result.Append(@"\cf5------------------" + CapitalFirst(cObject.ClassName) + @".swift------------------\line\line");
                

                result.Append(@"\cf12 class  \cf13" + cObject.ClassName + @"\line");
                result.Append(@"\cf12\{");

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

                result.Append(@"\cf12\}\line\line");
           
            }

            return result;
        }

        

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

                    result.Append(@"\tab \cf12 var \cf13" + (attribute.AttributeName ?? classString.ToLowerInvariant()) + @"\cf12  = [" + classString + @"]() \line ");
                    
                }
                // Or single attribute 
                else
                {
                    string classString = attribute.ClassObject.ClassName;
                    result.Append(@"\tab \cf12 var \cf13" + (attribute.AttributeName ?? classString.ToLower()) + @"\cf12: \cf13" + classString + @" \line");
                }
            }
        }

        private void TranslatePrimitiveAttributes(ClassObject cObject)
        {
            foreach (string attributeName in cObject.PrimitiveAttributes.Keys.ToList<string>())
            {
                string type;
                switch (cObject.PrimitiveAttributes[attributeName])
                {
                    case PrimitiveTypes.Boolean:
                        type = "Bool";
                        break;
                    case PrimitiveTypes.Float:
                        type = "Float";
                        break;
                    case PrimitiveTypes.Integer:
                        type = "Int";
                        break;
                    case PrimitiveTypes.String:
                        type = "String";
                        break;
                    default:
                        type = @"\cf10Type";
                        break;
                }
                result.Append(@"\tab \cf12" + "var "+ @"\cf13" + CapitalFirst(attributeName) + @"\cf12: " + type + @" \line");
            }
        }

        private void TranslateMethods(ClassObject cObject)
        {
            foreach (string methodName in cObject.Methods)
            {
                result.Append(@"\tab\cf12 func \cf13" + CapitalFirst(methodName) + @"\cf12() -> void\line");
                result.Append(@"\tab\{\line");
                result.Append(@"\line");
                result.Append(@"\tab\}\line");
            }
        }

        public override string GetLanguageName()
        {
            return "Swift";
        }

     

        
    }
}
