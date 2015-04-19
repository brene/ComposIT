using System;
using System.Collections.Generic;
using System.Text;
using ComposIT.Model;
using ComposIT.Model.Translation;
using ComposIT.Model.ClassStructure;
using System.Linq;
using Windows.UI.Xaml.Controls;
using Windows.UI.Text;
using System.Text.RegularExpressions;
using System.Diagnostics;
using System.IO;
using Windows.Storage;

namespace ComposIT.Model.Translation
{
    /// <summary>
    /// Manages the translation of the class tree into code
    /// </summary>
    public class Translator
    {
        /// <summary>
        /// The current language
        /// </summary>
        public TranslationStrategy CurrentLanguage;
        /// <summary>
        /// Contains all language and their respective names
        /// </summary>
        public Dictionary<string, TranslationStrategy> Languages;
        /// <summary>
        /// A list of all existing TranslationStrategy subclasses
        /// </summary>
        private static List<Type> _strategies = new List<Type>() 
        { 
            typeof(JavaTranslation), 
            typeof(CSharpTranslation),
            typeof(CppTranslation),
            typeof(SwiftTranslation)
        };


        /// <summary>
        /// Constructor that initializes all Strategies and attribtues 
        /// </summary>
        public Translator()
        {
            Languages = new Dictionary<string,TranslationStrategy>();

            foreach (Type type in _strategies)
            {
                TranslationStrategy strategy = (TranslationStrategy)Activator.CreateInstance(type);
                Languages.Add(strategy.GetLanguageName(), strategy);
            }

            CurrentLanguage = Languages.Values.First();
        }

        /// <summary>
        /// Starts the translation of class tree starting at the given root
        /// </summary>
        /// <param name="root">The root of the tree that will be translated to code</param>
        public void Translate(ClassObject root)
        {
            StringBuilder text = CurrentLanguage.Translate(root);
            ModelManager.GetInstance().TranslationChanged(CurrentLanguage.GetLanguageName(), text.ToString());
        }

        /// <summary>
        /// Exports the translated code without style elements into various files for each class.
        /// </summary>
        /// <param name="fileLocation">Location of the files that will be created</param>
        /// <returns>True if the export succeeded</returns>
        public async void Export(StorageFolder folder)
        {
            // Translate to plain text
            RichEditBox box = new RichEditBox();
            box.Document.SetText(TextSetOptions.FormatRtf, CurrentLanguage.result.ToString());
            string output;
            box.Document.GetText(TextGetOptions.UseCrlf, out output);

            // Split into pages and write into files
            string pattern = "-+([\\.\\w]+)[-\\s]+([^-]*)";
            Regex regex = new Regex(pattern);
            foreach (Match match in regex.Matches(output)) {
                string fileName = match.Groups[1].Value;
                string fileContent = match.Groups[2].Value;
                StorageFile file = await folder.CreateFileAsync(fileName, CreationCollisionOption.ReplaceExisting);
                await FileIO.WriteTextAsync(file, fileContent);
            }
        }

        /// <summary>
        /// Returns a list of all supported languages
        /// </summary>
        /// <returns>A list of all languages</returns>
        public List<string> GetLanguages()
        {
            return Languages.Keys.ToList();
        }

        /// <summary>
        /// Sets the current language.
        /// Illegal calls are ignored.
        /// </summary>
        /// <param name="language">The language that will used from now on</param>
        public void SetLanguage(string language)
        {
            if (!Languages.ContainsKey(language))
            {
                return;
            }
            CurrentLanguage = Languages[language];
        }

    }
}
