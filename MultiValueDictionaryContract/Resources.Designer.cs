﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MultiValueDictionaryContract {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("MultiValueDictionaryContract.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ) Cleared.
        /// </summary>
        internal static string ClearMsg {
            get {
                return ResourceManager.GetString("ClearMsg", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to (empty set).
        /// </summary>
        internal static string EmptySetMsg {
            get {
                return ResourceManager.GetString("EmptySetMsg", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ) ERROR, key does not exist.
        /// </summary>
        internal static string ErrorKeyNotExist {
            get {
                return ResourceManager.GetString("ErrorKeyNotExist", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ) ERROR, member already exists for key.
        /// </summary>
        internal static string ErrorMemberExists {
            get {
                return ResourceManager.GetString("ErrorMemberExists", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ) ERROR, member does not exist.
        /// </summary>
        internal static string ErrorMemberNotExists {
            get {
                return ResourceManager.GetString("ErrorMemberNotExists", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Invalid Argument.
        /// </summary>
        internal static string InvalidArgument {
            get {
                return ResourceManager.GetString("InvalidArgument", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ) Added.
        /// </summary>
        internal static string SuccessMsgAdded {
            get {
                return ResourceManager.GetString("SuccessMsgAdded", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ) Removed.
        /// </summary>
        internal static string SuccessMsgMemberRemoved {
            get {
                return ResourceManager.GetString("SuccessMsgMemberRemoved", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ) Removed.
        /// </summary>
        internal static string SuccessMsgMemberRemovedAll {
            get {
                return ResourceManager.GetString("SuccessMsgMemberRemovedAll", resourceCulture);
            }
        }
    }
}