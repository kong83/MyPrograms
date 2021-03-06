﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a TableCacheTemplate.tt T4 template.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
//     
//     DO NOT EDIT THIS FILE MANUALLY
// </auto-generated>
//------------------------------------------------------------------------------

// Disable missing XML comment for publicly visible type or member
#pragma warning disable 1591

using System;
using System.Linq;
using System.Diagnostics;
using System.Data.SqlClient;
using System.Collections.Generic;

namespace Confirmit.CATI.Core.DAL.Generated.Cache
{
    public class BvSurveyCache
    {
        public static readonly BvSurveyCache Instance = new BvSurveyCache();

        private readonly object m_lockObject = new object();

        private volatile bool m_isExpired;

        /// <summary>
        /// Private constructor needed for singleton implementation.
        /// </summary>
        private BvSurveyCache()
        {
            m_isExpired = true;
        }

        /// <summary>
        /// Returns cached table name.
        /// Needed to inform DatabaseTransactionScope 
        /// what tables changed in the transaction.
        /// </summary>
        public string CachedTableName
        {
            get
            {
                return "BvSurvey";
            }
        }

        /// <summary>
        /// If called inside transaction (inside DatabaseTransactionScope) then 
        /// registers cache to be notified after transaction successfull commit.
        /// After transaction commited DatabaseTransactionScope will call OnCacheExpired.
        /// 
        /// If called outside transaction (outside DatabaseTransactionScope) then
        /// expires the cache (calls OnCacheExpired immidiatelly.)
        /// </summary>
        public void OnTableChanged()
        {
            Console.WriteLine("test");
        }
    }
}