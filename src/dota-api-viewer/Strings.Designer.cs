﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DotaApiViewer {
    using System;
    using System.Reflection;
    
    
    /// <summary>
    ///    A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Strings {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        internal Strings() {
        }
        
        /// <summary>
        ///    Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("dota-api-viewer.Strings", typeof(Strings).GetTypeInfo().Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///    Overrides the current thread's CurrentUICulture property for all
        ///    resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to Access hero querying APIs..
        /// </summary>
        public static string HeroCommandDescription {
            get {
                return ResourceManager.GetString("HeroCommandDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to Access item querying APIs..
        /// </summary>
        public static string ItemCommandDescription {
            get {
                return ResourceManager.GetString("ItemCommandDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to No API Key could be found. Set DOTA_API_VIEWER_KEY directly to specify the key. Set DOTA_API_VIEWER_KEYFILE or place a &apos;{0}&apos; file next to this program containing the key..
        /// </summary>
        public static string KeyFileMissing {
            get {
                return ResourceManager.GetString("KeyFileMissing", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to The language to return localized strings in. Defaults to &apos;{0}&apos;..
        /// </summary>
        public static string LanguageOptionDescription {
            get {
                return ResourceManager.GetString("LanguageOptionDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to Access detailed match information..
        /// </summary>
        public static string MatchDetailsCommandDescription {
            get {
                return ResourceManager.GetString("MatchDetailsCommandDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to Access match history information..
        /// </summary>
        public static string MatchHistoryCommandDescription {
            get {
                return ResourceManager.GetString("MatchHistoryCommandDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to The name of the item or hero. If un-set, lists all heroes or items..
        /// </summary>
        public static string NameOptionDescription {
            get {
                return ResourceManager.GetString("NameOptionDescription", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to No heroes matched the name {0}. Check your spelling and language..
        /// </summary>
        public static string NoMatchingHeroes {
            get {
                return ResourceManager.GetString("NoMatchingHeroes", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to No items matched the name {0}. Check your spelling and language..
        /// </summary>
        public static string NoMatchingItems {
            get {
                return ResourceManager.GetString("NoMatchingItems", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to The web request failed. Error message: {0}.
        /// </summary>
        public static string RequestFailed {
            get {
                return ResourceManager.GetString("RequestFailed", resourceCulture);
            }
        }
        
        /// <summary>
        ///    Looks up a localized string similar to Access Steam player profile information..
        /// </summary>
        public static string SteamIDCommandDescription {
            get {
                return ResourceManager.GetString("SteamIDCommandDescription", resourceCulture);
            }
        }
    }
}
