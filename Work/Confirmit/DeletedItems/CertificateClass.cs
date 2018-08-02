using System;
/*using System.IO;
using System.Runtime.InteropServices;*/
using System.Security.Cryptography.X509Certificates;

namespace Confirmit.CATI.Setup.CustomAction
{/*
    enum CryptGetProvParamType
    {
        // ReSharper disable InconsistentNaming
        PP_ENUMALGS = 1,

        PP_ENUMCONTAINERS = 2,
        PP_IMPTYPE = 3,
        PP_NAME = 4,
        PP_VERSION = 5,
        PP_CONTAINER = 6,
        PP_CHANGE_PASSWORD = 7,
        PP_KEYSET_SEC_DESCR = 8,       // get/set security descriptor of keyset
        PP_CERTCHAIN = 9,              // for retrieving certificates from tokens
        PP_KEY_TYPE_SUBTYPE = 10,
        PP_PROVTYPE = 16,
        PP_KEYSTORAGE = 17,
        PP_APPLI_CERT = 18,
        PP_SYM_KEYSIZE = 19,
        PP_SESSION_KEYSIZE = 20,
        PP_UI_PROMPT = 21,
        PP_ENUMALGS_EX = 22,
        PP_ENUMMANDROOTS = 25,
        PP_ENUMELECTROOTS = 26,
        PP_KEYSET_TYPE = 27,
        PP_ADMIN_PIN = 31,
        PP_KEYEXCHANGE_PIN = 32,
        PP_SIGNATURE_PIN = 33,
        PP_SIG_KEYSIZE_INC = 34,
        PP_KEYX_KEYSIZE_INC = 35,
        PP_UNIQUE_CONTAINER = 36,
        PP_SGC_INFO = 37,
        PP_USE_HARDWARE_RNG = 38,
        PP_KEYSPEC = 39,
        PP_ENUMEX_SIGNING_PROT = 40,
        PP_CRYPT_COUNT_KEY_USE = 41,
        // ReSharper restore InconsistentNaming
    }
    */
    /// <summary>
    /// Class for work with certificates
    /// </summary>
    class CertificateClass
    {
        private readonly StoreName m_StoreName;
        private readonly StoreLocation m_StoreLocation;
        private readonly string m_CertificateKey;

        /// <summary>
        /// Get count of certificates
        /// </summary>        
        /// <returns></returns>
        public int GetCertificatesCount
        {
            get
            {
                return GetCertificates().Count;
            }
        }


        public CertificateClass(string storeName, string storeLocation)
            : this(storeName, storeLocation, null)
        {

        }


        public CertificateClass(string storeName, string storeLocation, string certificateKey)
        {
            m_StoreName = (StoreName)Enum.Parse(typeof(StoreName), storeName, true);
            m_StoreLocation = (StoreLocation)Enum.Parse(typeof(StoreLocation), storeLocation, true);
            m_CertificateKey = certificateKey;
        }


        /// <summary>
        /// Select certificate from selected store name and store location
        /// </summary>
        /// <returns></returns>
        public string SelectSertificateName()
        {
            var store = new X509Store(m_StoreName, m_StoreLocation);

            store.Open(OpenFlags.ReadOnly);
            try
            {
                var certs = X509Certificate2UI.SelectFromCollection(
                    store.Certificates,
                    "Real Certificate Select",
                    "Select real monitoring certificate",
                    X509SelectionFlag.SingleSelection);

                if (certs.Count == 1)
                {
                    return certs[0].Subject.Substring(3);
                }
                else
                {
                    return "";
                }
            }
            finally
            {
                store.Close();
            }
        }      


        /// <summary>
        /// Add certificate
        /// </summary>        
        /// <param name="certificateData">Certificate data</param>
        /// <returns></returns>
        public void AddCertificate(byte[] certificateData)
        {
            var certificate = new X509Certificate2(certificateData);
            var store = new X509Store(m_StoreName, m_StoreLocation);
            store.Open(OpenFlags.ReadWrite);
            store.Add(certificate);
        }

        /*
        /// <summary>
        /// Get certificates directory
        /// </summary>
        /// <returns></returns>
        public string GetCertificatePath()
        {
            X509Certificate2Collection certificates = GetCertificates();
            if (certificates.Count != 1)
            {
                throw new Exception(String.Format("Error: Count of certificates with key '{0}' is wrong: {1}", m_CertificateKey, certificates.Count));
            }            
            
            return GetKeyFilePath(certificates[0]);
        }*/


        /// <summary>
        /// Get certificates directory
        /// </summary>
        /// <returns></returns>
        public string GetCertificateThumbprint()
        {
            X509Certificate2Collection certificates = GetCertificates();
            if (certificates.Count != 1)
            {
                throw new Exception(String.Format("Error: Count of certificates with key '{0}' is wrong: {1}", m_CertificateKey, certificates.Count));
            }

            return certificates[0].Thumbprint;
        }


        /// <summary>
        /// Get sertificates list
        /// </summary>
        /// <returns></returns>
        private X509Certificate2Collection GetCertificates()
        {
            var store = new X509Store(m_StoreName, m_StoreLocation);
            try
            {
                store.Open(OpenFlags.ReadOnly);
                return store.Certificates.Find(X509FindType.FindBySubjectDistinguishedName, m_CertificateKey, false);
            }
            finally
            {
                store.Close();
            }
        }

        /*
        /// <summary>
        /// Get certificate file name
        /// </summary>
        /// <param name="cert">Certificate</param>
        /// <returns></returns>
        private static string GetKeyFileName(X509Certificate cert)
        {
            IntPtr hProvider = IntPtr.Zero;  // CSP handle
            bool freeProvider = false;       // Do we need to free the CSP ?
            const uint acquireFlags = 0;
            int keyNumber = 0;
            string keyFileName = null;
            byte[] keyFileBytes;

            //
            // Determine whether there is private key information available for this certificate in the key store
            //
            if (CryptAcquireCertificatePrivateKey(cert.Handle,
                acquireFlags,
                IntPtr.Zero,
                ref hProvider,
                ref keyNumber,
                ref freeProvider))
            {
                IntPtr pBytes = IntPtr.Zero; // Native Memory for the CRYPT_KEY_PROV_INFO structure
                int cbBytes = 0;             // Native Memory size

                try
                {
                    if (CryptGetProvParam(hProvider, CryptGetProvParamType.PP_UNIQUE_CONTAINER, IntPtr.Zero, ref cbBytes, 0))
                    {
                        pBytes = Marshal.AllocHGlobal(cbBytes);

                        if (CryptGetProvParam(hProvider, CryptGetProvParamType.PP_UNIQUE_CONTAINER, pBytes, ref cbBytes, 0))
                        {
                            keyFileBytes = new byte[cbBytes];

                            Marshal.Copy(pBytes, keyFileBytes, 0, cbBytes);

                            // Copy eveything except tailing null byte
                            keyFileName = System.Text.Encoding.ASCII.GetString(keyFileBytes, 0, keyFileBytes.Length - 1);
                        }
                    }
                }
                finally
                {
                    if (freeProvider)
                        CryptReleaseContext(hProvider, 0);

                    //
                    // Free our native memory
                    //
                    if (pBytes != IntPtr.Zero)
                        Marshal.FreeHGlobal(pBytes);

                }
            }

            if (keyFileName == null)
                throw new InvalidOperationException("Unable to obtain private key file name");

            return keyFileName;
        }


        /// <summary>
        /// Get certificate directory
        /// </summary>
        /// <param name="cert">Certificate</param>
        /// <returns></returns>
        private static string GetKeyFilePath(X509Certificate cert)
        {
            string keyFileName = GetKeyFileName(cert);

            // Look up All User profile from environment variable
            string allUserProfile = Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData);

            // set up searching directory
            string machineKeyDir = allUserProfile + "\\Microsoft\\Crypto\\RSA\\MachineKeys";

            // Seach the key file
            string[] fs = Directory.GetFiles(machineKeyDir, keyFileName);

            // If found
            if (fs.Length > 0)
                return machineKeyDir + "\\" + keyFileName;

            // Next try current user profile
            string currentUserProfile = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

            // seach all sub directory
            string userKeyDir = currentUserProfile + "\\Microsoft\\Crypto\\RSA\\";

            fs = Directory.GetDirectories(userKeyDir);
            if (fs.Length > 0)
            {
                // for each sub directory
                foreach (string keyDir in fs)
                {
                    fs = Directory.GetFiles(keyDir, keyFileName);
                    if (fs.Length == 0)
                        continue;

                    // found
                    return keyDir + "\\" + keyFileName;
                }
            }

            throw new InvalidOperationException("Unable to locate private key file directory");
        }


        [DllImport("crypt32", CharSet = CharSet.Unicode, SetLastError = true)]
        private extern static bool CryptAcquireCertificatePrivateKey(IntPtr pCert, uint dwFlags, IntPtr pvReserved, ref IntPtr phCryptProv, ref int pdwKeySpec, ref bool pfCallerFreeProv);

        [DllImport("advapi32", CharSet = CharSet.Unicode, SetLastError = true)]
        private extern static bool CryptGetProvParam(IntPtr hCryptProv, CryptGetProvParamType dwParam, IntPtr pvData, ref int pcbData, uint dwFlags);

        [DllImport("advapi32", SetLastError = true)]
        private extern static bool CryptReleaseContext(IntPtr hProv, uint dwFlags);*/
    }
}
