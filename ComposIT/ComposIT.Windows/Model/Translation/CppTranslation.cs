using ComposIT.Model.ClassStructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComposIT.Model.Translation
{
    public class CppTranslation : TranslationStrategy
    {
        /// <summary>
        /// Builds a class into a StringBuilder.
        /// </summary>
        /// <param name="cObject">the class to be build</param>
        /// <returns></returns>
        public override StringBuilder TranslateClass(ClassObject cObject)
        {
            //if the class is not MainClass and has a name.
            if (String.IsNullOrEmpty(cObject.ClassName) && !(cObject.GetType() == typeof(MainClassObject)))
            {

                result.Append(@"\cf5---------------------------NewClass.h--------------------------\line\line");
            }
            // if the class is MainClass
            else if (cObject.GetType() == typeof(MainClassObject))
            {
                result.Append(@"\cf5--------------------------MainClass.cpp--------------------------\line\line");
                result.Append(@"\cf8#include <iostream.h>\line\line");
                result.Append(@"\cf6int \cf1 main\cf6() \line\{\line");

                foreach (InstanceObject subObject in cObject.Attributes)
                {
                    //result.Append(@"\tab\cf1" + subObject.ClassObject.ClassName + @" \cf1" + subObject.AttributeName.ToLowerInvariant() + @"  \cf6 = new \cf1" + subObject.ClassObject.ClassName + @"\cf6();\line");
                }
                result.Append(@"\cf6\}\line\line");
                return result;
            }
            else
            {
                result.Append(@"\cf5--------------------------" + cObject.ClassName + ".h----------------------------");
                result.Append(@"\line");
            }
            
            result.Append(@"\line");

            //Generates the content of the header file.
            GenerateHeader(cObject);

            //if the class is not MainClass and has a name.
            if (String.IsNullOrEmpty(cObject.ClassName) && !(cObject.GetType() == typeof(MainClassObject)))
            {

                result.Append(@"\cf5---------------------------NewClass.cpp---------------------------\line\line");
            }
            
            else
            {
                result.Append(@"\cf5--------------------------" + cObject.ClassName + ".cpp----------------------------");
            }

            result.Append(@"\line");

            //Generates the content of the cpp file.
            GenerateCpp(cObject);

            return result;
        }

        /// <summary>
        /// Generates the header file of the class.
        /// </summary>
        /// <param name="cObject"></param>
        public void GenerateHeader(ClassObject cObject)
        {

            result.Append(@"\cf8#pragma once\line\line");
            result.Append(@"\cf6class \cf7 " + cObject.ClassName + @"\line\cf6\{\line private:\line");

            //builds the primitive attributes.
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
                        type = "type";
                        break;
                }
                result.Append(@"\cf6\tab " + type + @" \cf1" + attributeName + @"\cf6;\line\line");
            }

            //builds the other attributes.
         

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

                    result.Append(@"\tab\cf7 " + classString + @"[] \cf1" + attrString + @" \cf6= \{ ");
                    for (int i = 0; i < NumberOfInstances[attribute.ClassObject.Id]; i++)
                    {
                        if (i != 0)
                        {
                            result.Append(", ");
                        }
                        result.Append(@"new \cf1" + classString + @"\cf6()");
                    }
                    result.Append(@" \};\line");
                }
                // Or single attribute 
                else
                {
                    string classString = attribute.ClassObject.ClassName;
                    result.Append(@"\tab \cf6" + classString + @" \cf1" + (attribute.AttributeName ?? classString.ToLower()) + @"\cf6 ;\line\line");
                }
            }

            result.Append(@"\cf6 \line public: \line");
            //Constructors
            result.Append(@"\tab\cf1" + cObject.ClassName + @"\cf6();\line");
            //Destructors
            result.Append(@"\tab\cf1~" + cObject.ClassName + @"\cf6();\line");
                

            //getters and setters for the primitive attributes.
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
                        type = "String";
                        break;
                    default:
                        type = "type";
                        break;
                }
                //generate getter
                string toUpperCaseAttribute = char.ToUpper(attributeName[0]) + attributeName.Substring(1);
                result.Append(@"\tab\cf6" + type + @" \cf1 get" + toUpperCaseAttribute + @"\cf6();\line");

                //generate setter
                result.Append(@"\tab\cf6 void \cf1 set" + toUpperCaseAttribute + @"\cf6(\cf1" + type + " " + attributeName + @"\cf6);\line");
             
            }

            result.Append(@"\}\line");


        
            foreach (string methodName in cObject.Methods)
            {
                result.Append(@"\tab \cf1void " + methodName + @"\cf6();\line");
            
            }
        

        }


        /// <summary>
        /// Generates the cpp file of the class.
        /// </summary>
        /// <param name="cObject"></param>
        public void GenerateCpp(ClassObject cObject)
        {

            result.Append(@"\cf8\line" + "#include \"" + cObject.ClassName + ".h\"" + @"\line\line");

            //Constructors
            result.Append(@" \cf7 " + cObject.ClassName + @"\cf1::" + cObject.ClassName + @"\cf6() \{ \}\line");
            //Destructors
            result.Append(@" \cf7 " + cObject.ClassName + @"\cf1::~" + cObject.ClassName + @"\cf6() \{ \}\line");
                

         

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
                        type = "type";
                        break;
                }
                //generate getter
                string toUpperCaseAttribute = char.ToUpper(attributeName[0]) + attributeName.Substring(1);
                result.Append(@"\cf6 " + type + @" \cf7 " + cObject.ClassName + @"\cf6::" + @"\cf1get" + toUpperCaseAttribute + @"\cf6() \line\{\line");
                result.Append(@"\tab\cf6 return this\cf1->" + @"\cf1" + attributeName + @"\cf6;\line");
                result.Append(@"\cf6\}\line");
                result.Append(@"\line");

                //generate setter
                result.Append(@"\cf6 void \cf7 " + cObject.ClassName + @"\cf6::" + @"\cf1set" + toUpperCaseAttribute + @"\cf6(" + type + @" \cf1 " + attributeName + @"\cf6) \line\{\line");
                result.Append(@"\tab\cf6 this\cf1->" + @"\cf1" + attributeName + @" \cf6= " + attributeName + @";\line");
                result.Append(@"\cf6\}\line");
                result.Append(@"\line");
            }

            
            foreach (string methodName in cObject.Methods)
            {
                result.Append(@"\cf6 void \cf7 " + cObject.ClassName + @"\cf1::" + methodName + @"\cf6()\line");
                result.Append(@"{\line");
                result.Append(@"\line");
                result.Append(@"}\line");
            }
        

            result.Append(@"\line");

            
        }

        public override string GetLanguageName()
        {
            return "C++";
        }





        






    }
}
