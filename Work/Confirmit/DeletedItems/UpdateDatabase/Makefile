##---------------------------------------------------------------------------
## Copyright: (c) 1993/1996 Ulter Systems Ltd., Russia
## Copyright: (c) 1993/1996 Pulse Train Technology Ltd., UK
##---------------------------------------------------------------------------
## $Source: v:/archives/ApolloBuild/makefile.t $
## Unit: BuildLab
## Author: Serge_Zhukoff
## Responsible: Serge_Zhukoff
##---------------------------------------------------------------------------
## Description: Master makefile template
##---------------------------------------------------------------------------
## $Log: makefile.t $
## Revision 1.4  2006/06/22 19:36:56  Evgeniy_Shakhovskey
## Bug fix: If srcdefs contain USRTARGET, only these targets are built
## Revision 1.3  2006/01/30 15:43:48  Evgeniy_Shakhovskey
## Do not produce error messages for C# for CFG=Clean.
## Clean for C# is not implemented yet - should be.
## Revision 1.2  2006/01/13 18:40:22  Evgeniy_Shakhovskey
## Added "Release Debug" config to C#
## Revision 1.1  2006/01/13 16:53:21  Tom
## Initial revision
## Revision 1.56  2005/09/20 14:01:13  Evgeniy_Shakhovskey
## Nothing changed, only description added:
## - Apollo optimized build. srcdefs.inc instead of 3 other *.inc.
##   See objefs.pl and checkdep.pl for other details.
## - .NET (C#) support - see Make4Net.pl
##   TARGETTYPEs: CSLIB, CSEXE, CSWINEXE, CSMODULE
## - Added UNICODE support for CMDEXE
## - Changed PROJDIR to PROJPATH in the reference to makelib.bat
##   IF PROJPATH is not defined it will be \PROJECTS
##   changed "\PROJECTS" references to "$(PROJPATH)"
##   changed "/PROJECTS" references to "$(PROJPATH)"
## - OUTDIR and INTDIR will be redefined to default if they are equal to "."
## - added flag UNICODE_BUILD=False
## - added TARGETTYPE=DEPENDENCIES
##   Do not link if TARGETTYPE=DEPENDENCIES
## - MAKEDIR and CFG output
## - makedep output to $(MAKEDEP_OUT)
## - Added possibility to define common CFLAGS and LFLAGS
## - Added "Linking.." and "Building dependencies..." output
## - Removing empty $(OUTDIR) and IMPLIB
## - set OUTDIR to undefined if it is "." (invoked from studio)
## - checking dependencies for LIB
## - DEPEND: USRTARGET - optimized for $(MAKEDEP_OUT) output
## - Exit, if implicit rules are invoked. They should not.
##   This means USRTARGET or DEPENDENCIES were not built successfully
## - ANY USRTARGET could be defined in usrrules.inc.
##   They will be processesd at the very beginning
## - r suffix is optional in "* Release Debug" configurations.
##   It is not appended if R_SUFFIX=False
## - DEBUGINFO flag is added to "* Release Debug" configurations.
##   It overrides /Zi   option
## - Added "* Release Debug" configurations for CMDEXE target
## - If RELEASE_AS_RELEASEDEBUG=True and CFG="* Release", then CFG="* Rlease Debug"
## - !!! There was a need to introduce new variable CFGREDEFINED, as CFG variable
##   redefining does not work somehow ???
## - Two Flags for DEBUG instead of one now
##   DEBUGINFO include only /Z* flag now, nothing more
##   PDBINFO variable will overwrite path to PDB file if it is defined
## - OPTIMIZE FLAG will overwrite /O2 if it is defined
## - Moved LFLAGS definitions from TARGETS/CONFIGURATIONS to the top of file
##   for most of them
## - Removed /FIXED:NO from LFLAGS in most cases
## - Removed /D _MBCS from UNICODE configuration in ATLEXE target
## - Added Linker output
## - Excluded minimal rebuild option (/FD) from console targets
## - Added "Clean All" target, which means cleaning dependents as well,
##   while "Clean" target will clean only UNIT specified.
## - LFLAGS (LINK32_FLAGS) defined by user are the latest ones
## - added Date and Time to output (CURDATE and CURTIME variables)
## - defining LIBCFG for makelib for all TARGETTYPE, not only for LIB
## - do not call makelib, if LIB32_LIBS is empty
## - do not check if OUTDIR/INTDIR exist for "Clean" and "Clean All" targets
## - Changed slashes to backslashes in $(PROJPATH)
## - DIR_PREFIX=. for C++ (was undefined)
## - LIBCFG is defined for LIB (=CFG). Used for building dependencies
## - Removed slash before $(MAKEFLAGS) when calling makelib
## - Added variables CFLAGS_USR, RFLAGS_USR, LFLAGS_USR, which can be defined
##   prior to invoking build, and which will be passed to compilers and linker
##   at the very end of command line
## Revision 1.54  2004/05/13 12:28:47  Evgeniy_Shakhovskey
## MIXED_MFCEXE target added
## Revision 1.53  2004/04/26 13:04:42  Evgeniy_Shakhovskey
## Added target MIXED_DLL for VC7
## Revision 1.52  2004/04/23 15:02:09  Evgeniy_Shakhovskey
## Removed /GZ, /Z7, /YX for VC7
## Revision 1.51  2004/04/07 19:26:25  Evgeniy_Shakhovskey
## VC7 support. VC7 environment variable should be defined.
## Revision 1.50  2003/11/03 12:33:41  Evgeniy_Shakhovskey
## Added possibility to redefine CPP compiler and linker by setting
## CPP and LINK32 environment variables prior to invoking nmake utility
## Revision 1.49  2002/11/14 20:00:58  Evgeniy_Shakhovskey
## Added link flags for CMDEXE (incremental:no)
## Revision 1.48  2002/11/14 19:08:18  Evgeniy_Shakhovskey
## Bug fix in CMEXE target;
## CMDEXE added - Console multithreaded using DLLs
## Revision 1.47  2002/07/18 14:39:11  Evgeniy_Shakhovskey
## CMEXE (Console multithreading executable) target added
## Revision 1.46  2002/06/18 17:27:56  Evgeniy_Shakhovskey
## Added $(CFLAGS) from basename.inc options to OCX
## Revision 1.45  2002/05/28 18:27:20  Evgeniy_Shakhovskey
## Fixed bug for OCX with UNICODE support.
## Revision 1.44  2002/04/27 12:15:29  Evgeniy_Shakhovskey
## Excluded /GZ from Release Debug
## Revision 1.43  2002/04/26 11:15:40  Andrey_Timoshenkov
## INCLUDE quoted for makedep
## Revision 1.42  2002/04/25 18:36:04  Evgeniy_Shakhovskey
## /GZ Added to Release Debug configurations
## Revision 1.41  2002/04/25 16:51:33  Evgeniy_Shakhovskey
## /GZ Added to Debug configurations
## Revision 1.40  2002/04/17 12:48:47  Evgeniy_Shakhovskey
## ATLDLL Debug Release added
## Revision 1.39  2002/04/01 15:00:16  Andrey_Timoshenkov
## Slash for MAKEFLAGS. INCLUDE for makedep.
## Revision 1.38  2001/04/06 14:46:28  Evgeniy_Shakhovskey
## 1. UNICODE support for all the targets
## 2. added _REL_DEB definition (to CPP_PROJ) for Release Debug configurations
## Revision 1.37  2001/03/28 15:48:18  Evgeniy_Shakhovskey
## Added "Release Debug" target
## Revision 1.36  2001/02/01 18:13:11  Evgeniy_Shakhovskey
## Removed /entry:"wWinMainCRTStartup" for UNICODE ATLDLL target
## Revision 1.35  2001/01/25 20:41:21  Evgeniy_Shakhovskey
## OUTEXT may be defined
## Revision 1.34  2000/12/18 19:20:52  Evgeniy_Shakhovskey
## 1. Bug fix. OCX Unicode Release added.
## Revision 1.33  2000/11/04 18:05:02  Evgeniy_Shakhovskey
## 1. ATLDLL target added
## 2. CFG="Release Debug" added (Release with debug info
## Revision 1.32  2000/10/06 15:29:17  Evgeniy_Shakhovskey
## 1. Bug fix in ATLEXE
## 2. Added UNICODE support for ATLEXE
## Revision 1.31  2000/09/22 19:52:45  Evgeniy_Shakhovskey
## Changed _UNICODE_BUILDLAB_ env var to UNIBUILD, as nmake
## does not understand the first one
## Revision 1.30  2000/09/13 18:18:33  Evgeniy_Shakhovskey
## Bug fix for ATLEXE: $(CFLAGS) was absent in CPP_PROJ
## Revision 1.29  2000/08/24 19:22:05  Evgeniy_Shakhovskey
## TARGETTYPE=ATLEXE added
## Revision 1.28  2000/08/24 16:15:10  Evgeniy_Shakhovskey
## UNICODE support
## Revision 1.27  1999/01/15 16:08:54  Library_Builder
## Changed IFDEF VC4 to IFDEF VC2
## Revision 1.26  1998/08/27 13:43:56  evgeniy
## Trancated space from _WINDLL definition
## Revision 1.25  1998/08/27 13:37:08  evgeniy
## Changed /MD with /MT for DLL_SCAN
## Revision 1.24  1998/08/27 13:03:46  evgeniy
## INTDIR may be set not to any other than default
## Revision 1.23  1998/08/27 11:17:54  Serge_Zhukoff
## Added DLL_SCAN target to build peculiar SCAN DLLs
## Revision 1.22  1997/10/31 11:38:37  Quality_Assurance
## OCX are "buildabe" now
## Revision 1.21.1.2  1997/10/02 16:02:47  Quality_Assurance
## OCX are "buildabe" now
## Revision 1.21  1996/09/02 11:55:27  Serge_Zhukoff
## 1.Now using debugging allocation when building single-threaded EXEs & LIBs
## 2.Not building library dependencies if symbol NODEPENDENTBUILD is defined
## Revision 1.20  1996/06/13 17:20:36  Quality_Assurance
## Changed logging of the ${UNIT} to logging of the ${BASENAME}
## in case of LIGHT building.
## Revision 1.19  1996/06/11 21:06:30  Evgeniy_Shakhovskey
## Corrected the registration form
## Revision 1.18  1996/05/28 17:52:01  Serge_Zhukoff
## Flavour inhibition support; deleting .exp files
## Revision 1.17  1996/05/06 20:20:21  Evgeniy_Shakhovskey
## Logging through shell
## Revision 1.17  1996/05/06 20:05:57  Evgeniy_Shakhovskey
## Revision 1.16  1996/04/25 10:27:08  Evgeniy_Shakhovskey
## Added logging for 'none-strict' assembling.
## 'None-strict' means, that some units can't be built due to the bugs,
## but build won't stop, it will try to assemble the binaries for
## other units. genmkfrt should be given 'independent' parameter
## for 'none-strict' assembling.
## Copying from PROJECTS to SECURITY won't be performed,
## in case of 'none-strict' assembling.
## LIGHT env var should be set to YES for 'none-strict' assembling.
## Revision 1.15  1996/04/23 11:48:02  Quality_Assurance
## Serge added VC++4.0 support (see VC4 variable)
## Revision 1.14  1996/04/18 19:00:44  Quality_Assurance
## Just the same as 1.12 (without logging into \projects\log)
## Revision 1.12  1996/04/18 19:00:45  Serge_Zhukoff
## Separated LINK32_FLAGS and LINK32_LIBS; added support for DEFAULTLIBS
## Revision 1.11  1996/04/16 16:28:54  Serge_Zhukoff
## Added /I \projects\units to the commandline of the resource compiler
## Revision 1.10  1996/04/10 11:48:26  Alexander_Pivovarov
## Added Config Variable DIR_SUFFIX.
## Could be used to store separately files
## generated by MSVC 2.0 and MSVC 4.0
## Revision 1.9  1996/04/08 12:24:06  Serge_Zhukoff
## Changed makedep invocation so that be able to proceed
## huge number of files
## Revision 1.8  1996/04/01 18:59:21  Serge_Zhukoff
## Revision 1.7  1996/02/27 14:28:50  Serge_Zhukoff
## Added definition of IMPLIBSUFFIX
## Revision 1.6  1996/02/12 14:44:02  Serge_Zhukoff
## Removed the definition of AFXDLL for static DLLs
## Revision 1.5  1996/02/12 08:19:08  Serge_Zhukoff
## Added the USRTARGET evaluation
## Revision 1.4  1996/01/09 15:58:50  Marina_Choumenko
## Added /I $(EIFFEL_S)/RUN3 to all alternatives
## Revision 1.3  1995/12/26 16:35:20  Dmitri_Ivanov
## Checking and defaulting configuration $(CFG) moved above !INCLUDE "basename.inc"
## Removed generation of .SBR files as MSVC Browser Symbol Cache (BSC) is not supported
##---------------------------------------------------------------------------

!IF "$(CFG)" == ""
CFG=Win32 Debug
!MESSAGE No configuration specified.  Defaulting to "$(CFG)".
!ENDIF

!IF "$(CFG)" != "Win32 Debug" && "$(CFG)" != "Win32 Release" \
       && "$(CFG)" != "Win32 Release Debug"                  \
       && "$(CFG)" != "Win32 MT Debug"                       \
       && "$(CFG)" != "Win32 MT Release"                     \
       && "$(CFG)" != "Win32 MT Release Debug"               \
       && "$(CFG)" != "Win32 MTD Debug"                      \
       && "$(CFG)" != "Win32 MTD Release"                    \
       && "$(CFG)" != "Win32 MTD Release Debug"              \
       && "$(CFG)" != "Win32 Unicode Debug"                  \
       && "$(CFG)" != "Win32 Unicode Release"                \
       && "$(CFG)" != "Win32 Unicode Release Debug"          \
       && "$(CFG)" != "Win32 MT Unicode Debug"               \
       && "$(CFG)" != "Win32 MT Unicode Release"             \
       && "$(CFG)" != "Win32 MT Unicode Release Debug"       \
       && "$(CFG)" != "Win32 MTD Unicode Debug"              \
       && "$(CFG)" != "Win32 MTD Unicode Release"            \
       && "$(CFG)" != "Win32 MTD Unicode Release Debug"      \
       && "$(CFG)" != "Clean"                                \
       && "$(CFG)" != "Clean All"
!ERROR An invalid configuration "$(CFG)" is specified.
!ENDIF

#       Define flavours

ST_FLAVOUR = 1
MT_FLAVOUR = 1
MD_FLAVOUR = 1

################################################################################
# Here the base name of the library should go
################################################################################

TARGET0: TARGET1

#!INCLUDE "basename.inc"
!IF EXIST("srcdefs.inc")
!IF EXIST("basename.inc")
!ERROR "basename.inc" should not live in the same catalogue with "srcdefs.inc" - $(MAKEDIR)
!ENDIF
!IF EXIST("filedefs.inc")
!ERROR "filedefs.inc" should not live in the same catalogue with "srcdefs.inc" - $(MAKEDIR)
!ENDIF
#!IF EXIST("usrrules.inc")
#!ERROR "usrrules.inc" should not live in the same catalogue with "srcdefs.inc" - $(MAKEDIR)
#!ENDIF
!ENDIF

!IF EXIST("srcdefs.inc")
!INCLUDE "srcdefs.inc"
MAKELIB=$(PROJPATH)\makelib.bat
!IF EXIST("objdefs.inc")
!INCLUDE "objdefs.inc"
#!ELSE
#!ERROR Can not find 'objdefs.inc' file
!ENDIF
!ELSE IF EXIST("basename.inc")
!INCLUDE "basename.inc"
MAKELIB=$(PROJPATH)\makelib.bat
!ELSE
!ERROR "No files with source definitions found"
!ENDIF

!IFNDEF PROJDIR
PROJDIR=\PROJECTS
!ENDIF

!IF "$(PROJPATH)" == ""
PROJPATH=\PROJECTS
!ENDIF
PROJPATH=$(PROJPATH:/=\)

!IF "$(R_SUFFIX)" == "False"
R_SUFF=
!ELSE
R_SUFF=r
!ENDIF

!IF "$(D_SUFFIX)" == "False"
D_SUFF=
!ELSE
D_SUFF=d
!ENDIF

# CFG=Something does not work somehow !
# There is a need to introduce another variable - CFGREDEFINED
#
CFGREDEFINED = $(CFG)

!IF "$(RELEASE_AS_RELEASEDEBUG)" == "True"
!IF "$(CFG)" == "Win32 Release"
CFGREDEFINED = Win32 Release Debug
!ELSEIF "$(CFG)" == "Win32 MT Release"
CFGREDEFINED = Win32 MT Release Debug
!ELSEIF "$(CFG)" == "Win32 MTD Release"
CFGREDEFINED = Win32 MTD Release Debug
!ELSEIF "$(CFG)" == "Win32 Unicode Release"
CFGREDEFINED =Win32 Unicode Release Debug
!ELSEIF "$(CFG)" == "Win32 MT Unicode Release"
CFGREDEFINED = Win32 MT Unicode Release Debug
!ELSEIF "$(CFG)" == "Win32 MTD Unicode Release"
CFGREDEFINED = Win32 MTD Unicode Release Debug
!ELSE
!ENDIF
!ENDIF

!IF "$(DEBUGINFO)" == ""

!IF "$(CFGREDEFINED)" == "Win32 Release Debug"                   \
       || "$(CFGREDEFINED)" == "Win32 MT Release Debug"          \
       || "$(CFGREDEFINED)" == "Win32 MTD Release Debug"         \
       || "$(CFGREDEFINED)" == "Win32 Unicode Release Debug"     \
       || "$(CFGREDEFINED)" == "Win32 MT Unicode Release Debug"  \
       || "$(CFGREDEFINED)" == "Win32 MTD Unicode Release Debug"
DEBUGINFO = /Zi
!ELSE
DEBUGINFO = /Z7
!ENDIF

!ELSE # $(DEBUGINFO) is defined (Bv Fusion use it)

!ENDIF

!IFNDEF VC7

!IF "$(CFGREDEFINED)" == "Win32 Release"                   \
       || "$(CFGREDEFINED)" == "Win32 MT Release"          \
       || "$(CFGREDEFINED)" == "Win32 MTD Release"         \
       || "$(CFGREDEFINED)" == "Win32 Unicode Release"     \
       || "$(CFGREDEFINED)" == "Win32 MT Unicode Release"  \
       || "$(CFGREDEFINED)" == "Win32 MTD Unicode Release"
#PDBINFO=
LFLAGS=/PDB:NONE $(LFLAGS)
!ELSEIF "$(DEBUGINFO)" == "/Z7"
#PDBINFO=
LFLAGS=/DEBUG /PDB:NONE $(LFLAGS)
!ELSEIF "$(DEBUGINFO)" == "/Zi"
!IF "$(PDBINFO)" == ""
PDBINFO=/Fd$(OUTDIR)/$(OUTNAME).PDB
!ENDIF
LFLAGS=/DEBUG /PDB:$(PDBINFO:/Fd=) /PDBTYPE:SEPT $(LFLAGS)
!ELSE
!ERROR Wrong DEBUGINFO variable value - "$(DEBUGINFO)"
!ENDIF

!ENDIF

###!IF "$(PDBINFO)" == ""
###
###!IF "$(CFGREDEFINED)" == "Win32 Release"                   \
###       || "$(CFGREDEFINED)" == "Win32 MT Release"          \
###       || "$(CFGREDEFINED)" == "Win32 MTD Release"         \
###       || "$(CFGREDEFINED)" == "Win32 Unicode Release"     \
###       || "$(CFGREDEFINED)" == "Win32 MT Unicode Release"  \
###       || "$(CFGREDEFINED)" == "Win32 MTD Unicode Release"
###PDBINFO=
###LFLAGS=/PDB:NONE $(LFLAGS)
###!ELSEIF "$(DEBUGINFO)" == "/Z7"
###PDBINFO=
###LFLAGS=/DEBUG /PDB:NONE $(LFLAGS)
###!ELSEIF "$(DEBUGINFO)" == "/Zi"
###PDBINFO=/Fd$(OUTDIR)/$(OUTNAME).PDB
###LFLAGS=/DEBUG /PDB:$(OUTDIR)/$(OUTNAME).PDB /PDBTYPE:SEPT $(LFLAGS)
###!ELSE
###!ERROR Wrong DEBUGINFO variable value - "$(DEBUGINFO)"
###!ENDIF
###
###!ELSE # "$(PDBINFO)" != ""
###
###!IF "$(CFGREDEFINED)" == "Win32 Release"                   \
###       || "$(CFGREDEFINED)" == "Win32 MT Release"          \
###       || "$(CFGREDEFINED)" == "Win32 MTD Release"         \
###       || "$(CFGREDEFINED)" == "Win32 Unicode Release"     \
###       || "$(CFGREDEFINED)" == "Win32 MT Unicode Release"  \
###       || "$(CFGREDEFINED)" == "Win32 MTD Unicode Release"
###PDBINFO=
###LFLAGS=/PDB:NONE $(LFLAGS)
###!ELSEIF "$(DEBUGINFO)" == "/Z7"
###PDBINFO=
###LFLAGS=/DEBUG /PDB:NONE $(LFLAGS)
###!ELSEIF "$(DEBUGINFO)" == "/Zi"
####PDBINFO=/Fd$(OUTDIR)/$(OUTNAME).PDB
###LFLAGS=/DEBUG $(PDBINFO) /PDBTYPE:SEPT $(LFLAGS)
###!ELSE
###!ERROR Wrong DEBUGINFO variable value - "$(DEBUGINFO)"
###!ENDIF
###
###!ENDIF # "$(PDBINFO)" == ""

#!MESSAGE LFLAGS "$(LFLAGS)"

!IF "$(OPTIMIZE)" == ""
OPTIMIZE=/O2
!ENDIF

!IF "$(UNICODE_BUILD)" == "False"
UNIBUILD=False
UNICODE_FLAGS= /D "_MBCS"
!ELSE
UNICODE_FLAGS = /D "_UNICODE" /D "UNICODE"
!ENDIF

CFLAGS = $(CFLAGS_COMMON) $(CFLAGS)
RFLAGS = $(RFLAGS_COMMON) $(RFLAGS)
LFLAGS = $(LFLAGS_COMMON) $(LFLAGS)

!IF "$(CFGREDEFINED)" == "Win32 Debug"  \
       || "$(CFGREDEFINED)" == "Win32 MT Debug"                       \
       || "$(CFGREDEFINED)" == "Win32 MTD Debug"                      \
       || "$(CFGREDEFINED)" == "Win32 Unicode Debug"                  \
       || "$(CFGREDEFINED)" == "Win32 MT Unicode Debug"               \
       || "$(CFGREDEFINED)" == "Win32 MTD Unicode Debug"
!IFNDEF VC7
CFLAGS = /GZ $(CFLAGS)
!ENDIF
!ENDIF

################################################################################

!IF "$(TARGETTYPE)" == ""
TARGETTYPE=$(DEFTARGETTYPE)
!ENDIF
!IF "$(TARGETTYPE)" == ""
!MESSAGE No target type specified.  Defaulting to static library.
TARGETTYPE=LIB
!ENDIF

!IF "$(TARGETTYPE)" != "LIB"                 \
       && "$(TARGETTYPE)" != "DLL"           \
       && "$(TARGETTYPE)" != "ATLDLL"        \
       && "$(TARGETTYPE)" != "DLL_STATIC"    \
       && "$(TARGETTYPE)" != "EXE"           \
       && "$(TARGETTYPE)" != "MFCEXE"        \
       && "$(TARGETTYPE)" != "MFCEXE_STATIC" \
       && "$(TARGETTYPE)" != "ATLEXE"        \
       && "$(TARGETTYPE)" != "OCX"           \
       && "$(TARGETTYPE)" != "DLL_SCAN"      \
       && "$(TARGETTYPE)" != "CMEXE"         \
       && "$(TARGETTYPE)" != "CMDEXE"        \
       && "$(TARGETTYPE)" != "MIXED_DLL"     \
       && "$(TARGETTYPE)" != "MIXED_MFCEXE"  \
       && "$(TARGETTYPE)" != "TYPLIB"        \
       && "$(TARGETTYPE)" != "DEPENDENCIES"  \
       && "$(TARGETTYPE)" != "CSLIB"         \
       && "$(TARGETTYPE)" != "CSEXE"         \
       && "$(TARGETTYPE)" != "CSWINEXE"      \
       && "$(TARGETTYPE)" != "CSMODULE"
!ERROR An invalid target type is specified.
!ENDIF

!IF "$(TARGETTYPE)"       == "CSLIB"         \
       || "$(TARGETTYPE)" == "CSEXE"         \
       || "$(TARGETTYPE)" == "CSWINEXE"      \
       || "$(TARGETTYPE)" == "CSMODULE"

CFG_CS=$(CFGREDEFINED: MTD=)
CFG_CS=$(CFG_CS: MT=)
CFG_CS=$(CFG_CS: Unicode=)
#CFG_CS=$(CFG_CS:Release Debug=Release)
#!ERROR CFG=$(CFG_CS)

#CS=$(SYSTEMROOT)\Microsoft.NET\Framework\v3.5\csc.exe

# ========================================================= C#

CS=$(SYSTEMROOT)\Microsoft.NET\Framework\v3.5\csc.exe

#!IF "$(DIR_PREFIX)" == ""
#!IF "$(CFG)" == "Win32 Debug"
#DIR_PREFIX=Debug
#!ELSE
#DIR_PREFIX=Release
#!ENDIF
#!ENDIF

!IF "$(INPUT4PERL)" != ""
xINPUT4PERL="$(INPUT4PERL)"
!ENDIF
!IF "$(INPUT4MAKE)" != ""
xINPUT4MAKE="$(INPUT4MAKE)"
!ENDIF
!IF "$(INPUT4CS)" != ""
xINPUT4CS="$(INPUT4CS)"
!ENDIF

!ELSE
DIR_PREFIX=.
!ENDIF

!IFDEF VC7
VC7=1
MAKEFLAGS="$(MAKEFLAGS) VC7=1"
#DEFINE VC6
CFLAGS=/D _WIN32_WINNT=0x0400 $(CFLAGS)
VC6=1
!ENDIF

!IFDEF VC2
MTD=MT
MDD=MD
MLD=
!ELSE
MLD=/MLd
MTD=MTd
MDD=MDd
!ENDIF

#.IGNORE:
!IFDEF VC4
MAKEFLAGS="$(MAKEFLAGS) VC4=1"
!ENDIF
!IFDEF VC6
MAKEFLAGS="$(MAKEFLAGS) VC6=1"
!ENDIF

#
#   Select an intermediate directory according to configuration
#

!IF "$(DEFTARGETTYPE)" == "DLL" || "$(DEFTARGETTYPE)" == "DLL_STATIC"
AFXDLL=/D _AFXEXT
!ELSEIF "$(DEFTARGETTYPE)" == "DLL_SCAN"
AFXDLL=/D "_WINDLL" /D "_USRDLL" /D "AFX_APP_DLL" /D "_MBCS"
!ELSE
AFXDLL=/D _AFXDLL
!ENDIF

!IF "$(OUTDIR)" == "."
OUTDIR=
!ENDIF

CFLAGS=$(CFLAGS) $(CFLAGS_USR)
LFLAGS=$(LFLAGS) $(LFLAGS_USR)
RFLAGS=$(RFLAGS) $(RFLAGS_USR)

# ----------------------------------------------------- TYPLIB
!IF "$(TARGETTYPE)" == "TYPLIB"

LIBCFG=$(CFGREDEFINED)

!ELSEIF "$(TARGETTYPE)" == "DEPENDENCIES"

LIBCFG=$(CFGREDEFINED)

# ------------------ OCX -------------------------

!ELSEIF "$(TARGETTYPE)" == "OCX"

!IF "$(CFGREDEFINED)" != "Win32 Unicode Release"                   \
        && "$(CFGREDEFINED)" != "Win32 MT Unicode Release"         \
        && "$(CFGREDEFINED)" != "Win32 MTD Unicode Release"        \
        && "$(CFGREDEFINED)" != "Win32 Unicode Release Debug"      \
        && "$(CFGREDEFINED)" != "Win32 MT Unicode Release Debug"   \
        && "$(CFGREDEFINED)" != "Win32 MTD Unicode Release Debug"  \
        && "$(CFGREDEFINED)" != "Win32 Unicode Debug"              \
        && "$(CFGREDEFINED)" != "Win32 MT Unicode Debug"           \
        && "$(CFGREDEFINED)" != "Win32 MTD Unicode Debug"
CFLAGS=/D "_MBCS" $(CFLAGS)
!ENDIF
CFLAGS=/D "_WINDLL" /D "_USRDLL" $(CFLAGS)

!IF "$(OUTDIR)" == "" || "$(OUTDIR)" == "."
OUTDIR=$(PROJPATH)\$(DIR_PREFIX)\DLL$(DIR_SUFFIX)
!ENDIF

!IF "$(OUTEXT)" == ""
OUTEXT=ocx
!ENDIF
SUBSYSTEM=windows /DLL /IMPLIB:$(PROJPATH)/IMPLIB$(DIR_SUFFIX)/$(OUTNAME).LIB

!IF "$(CFGREDEFINED)" == "Win32 Release" || "$(CFGREDEFINED)" == "Win32 MT Release" || "$(CFGREDEFINED)" == "Win32 MTD Release"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinRel
!ENDIF
LIBSUFFIX=l
IMPLIBSUFFIX=
LIBCFG=Win32 MTD Release
OUTNAME=$(BASENAME)
CPP_PROJ=/nologo /MD /W3 /GX /YX $(OPTIMIZE) /D "WIN32" /D "NDEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3  /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c $(AFXDLL) $(CFLAGS)
#LFLAGS=/FIXED:NO
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res /d "NDEBUG" /d "_AFXDLL" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
!ENDIF

!IF "$(CFGREDEFINED)" == "Win32 Release Debug" || "$(CFGREDEFINED)" == "Win32 MT Release Debug" || "$(CFGREDEFINED)" == "Win32 MTD Release Debug"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinRelDeb
!ENDIF
LIBSUFFIX=l$(R_SUFF)
IMPLIBSUFFIX=$(R_SUFF)
LIBCFG=Win32 MTD Release Debug
OUTNAME=$(BASENAME)$(R_SUFF)
CPP_PROJ=/nologo /MD /W3 /GX /YX $(OPTIMIZE) /D "_REL_DEB" /D "WIN32" /D "NDEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3 /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c $(AFXDLL) $(DEBUGINFO) $(PDBINFO) $(CFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res /d "NDEBUG" /d "_AFXDLL" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
#LFLAGS=/FIXED:NO $(LFLAGS)
!ENDIF

!IF "$(CFGREDEFINED)" == "Win32 Unicode Release" || "$(CFGREDEFINED)" == "Win32 MT Unicode Release" || "$(CFGREDEFINED)" == "Win32 MTD Unicode Release"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinRelU
!ENDIF
LIBSUFFIX=lu
IMPLIBSUFFIX=u
LIBCFG=Win32 MTD Unicode Release
OUTNAME=$(BASENAME)u
!IF EXIST("unicode.no")
CPP_PROJ=/nologo /MD /W3 /GX /YX $(OPTIMIZE) /D "WIN32" /D "NDEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3  /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c $(AFXDLL) $(CFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res /d "NDEBUG" /d "_AFXDLL" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
!ELSE
CPP_PROJ=/nologo /MD /W3 /GX /YX $(OPTIMIZE) $(UNICODE_FLAGS) /D "WIN32" /D "NDEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3  /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c $(AFXDLL) $(CFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res $(UNICODE_FLAGS) /d "NDEBUG" /d "_AFXDLL" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
!ENDIF
!ENDIF

!IF "$(CFGREDEFINED)" == "Win32 Unicode Release Debug" || "$(CFGREDEFINED)" == "Win32 MT Unicode Release Debug" || "$(CFGREDEFINED)" == "Win32 MTD Unicode Release Debug"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinRelDebU
!ENDIF
LIBSUFFIX=lu$(R_SUFF)
IMPLIBSUFFIX=u$(R_SUFF)
LIBCFG=Win32 MTD Unicode Release Debug
OUTNAME=$(BASENAME)u$(R_SUFF)
CPP_PROJ=/nologo /MD /W3 /GX /YX $(OPTIMIZE) /D "_REL_DEB" $(UNICODE_FLAGS) /D "WIN32" /D "NDEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3  /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c $(AFXDLL) $(DEBUGINFO) $(PDBINFO) $(CFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res $(UNICODE_FLAGS) /d "NDEBUG" /d "_AFXDLL" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
#LFLAGS=/FIXED:NO $(LFLAGS)
!ENDIF

!IF "$(CFGREDEFINED)" == "Win32 Debug" || "$(CFGREDEFINED)" == "Win32 MT Debug" || "$(CFGREDEFINED)" == "Win32 MTD Debug"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinDebug
!ENDIF
LIBSUFFIX=l$(D_SUFF)
IMPLIBSUFFIX=$(D_SUFF)
LIBCFG=Win32 MTD Debug
#LFLAGS=/DEBUG:FULL $(LFLAGS)
OUTNAME=$(BASENAME)$(D_SUFF)
CPP_PROJ=/nologo /$(MDD) /W3 /GX $(DEBUGINFO) $(PDBINFO) /YX /Od /D "WIN32" /D "_DEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3  /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c $(AFXDLL) $(CFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res /d "_DEBUG" /d "_AFXDLL"  /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
!ENDIF

!IF "$(CFGREDEFINED)" == "Win32 Unicode Debug" || "$(CFGREDEFINED)" == "Win32 MT Unicode Debug" || "$(CFGREDEFINED)" == "Win32 MTD Unicode Debug"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinDebU
!ENDIF
LIBSUFFIX=lu$(D_SUFF)
IMPLIBSUFFIX=u$(D_SUFF)
LIBCFG=Win32 MTD Unicode Debug
#LFLAGS=/DEBUG:FULL $(LFLAGS)
OUTNAME=$(BASENAME)u$(D_SUFF)
!IF EXISTS("unicode.no)
CPP_PROJ=/nologo /$(MDD) /W3 /GX $(DEBUGINFO) $(PDBINFO) /YX /Od /D "WIN32" /D "_DEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3  /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c $(AFXDLL) $(CFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res /d "_DEBUG" /d "_AFXDLL" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
!ELSE
CPP_PROJ=/nologo /$(MDD) /W3 /GX $(DEBUGINFO) $(PDBINFO) /YX /Od $(UNICODE_FLAGS) /D "WIN32" /D "_DEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3  /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c $(AFXDLL) $(CFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res $(UNICODE_FLAGS) /d "_DEBUG" /d "_AFXDLL" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
!ENDIF
!ENDIF

# ------------------ OCX END -------------------------

!ELSEIF "$(TARGETTYPE)" == "DLL"

!IF "$(OUTDIR)" == "" || "$(OUTDIR)" == "."
OUTDIR=$(PROJPATH)\$(DIR_PREFIX)\DLL$(DIR_SUFFIX)
!ENDIF

!IF "$(OUTEXT)" == ""
OUTEXT=DLL
!ENDIF
SUBSYSTEM=windows /DLL /IMPLIB:$(PROJPATH)/IMPLIB$(DIR_SUFFIX)/$(OUTNAME).LIB

!IF "$(CFGREDEFINED)" == "Win32 Release" || "$(CFGREDEFINED)" == "Win32 MT Release" || "$(CFGREDEFINED)" == "Win32 MTD Release"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinRel
!ENDIF
LIBSUFFIX=l
IMPLIBSUFFIX=
LIBCFG=Win32 MTD Release
OUTNAME=$(BASENAME)
CPP_PROJ=/nologo /MD /W3 /GX /YX $(OPTIMIZE) /D "WIN32" /D "NDEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3  /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c $(AFXDLL) $(CFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res /d "NDEBUG" /d "_AFXDLL" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
!ENDIF

!IF "$(CFGREDEFINED)" == "Win32 Release Debug" || "$(CFGREDEFINED)" == "Win32 MT Release Debug" || "$(CFGREDEFINED)" == "Win32 MTD Release Debug"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinRelDeb
!ENDIF
LIBSUFFIX=l$(R_SUFF)
IMPLIBSUFFIX=$(R_SUFF)
LIBCFG=Win32 MTD Release Debug
OUTNAME=$(BASENAME)$(R_SUFF)
CPP_PROJ= $(PDB_COMPILER) /nologo /MD /W3 /GX /YX $(OPTIMIZE) /D "_REL_DEB" /D "WIN32" /D "NDEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3  /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c $(AFXDLL) $(DEBUGINFO) $(PDBINFO) $(CFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res /d "NDEBUG" /d "_AFXDLL" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
!ENDIF

!IF "$(CFGREDEFINED)" == "Win32 Unicode Release" || "$(CFGREDEFINED)" == "Win32 MT Unicode Release" || "$(CFGREDEFINED)" == "Win32 MTD Unicode Release"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinRelU
!ENDIF
LIBSUFFIX=lu
IMPLIBSUFFIX=u
LIBCFG=Win32 MTD Unicode Release
OUTNAME=$(BASENAME)u
!IF EXIST("unicode.no")
CPP_PROJ=/nologo /MD /W3 /GX /YX $(OPTIMIZE) /D "WIN32" /D "NDEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3  /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c $(AFXDLL) $(CFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res /d "NDEBUG" /d "_AFXDLL" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
!ELSE
CPP_PROJ=/nologo /MD /W3 /GX /YX $(OPTIMIZE) $(UNICODE_FLAGS) /D "WIN32" /D "NDEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3  /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c $(AFXDLL) $(CFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res $(UNICODE_FLAGS) /d "NDEBUG" /d "_AFXDLL" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
!ENDIF
!ENDIF

!IF "$(CFGREDEFINED)" == "Win32 Unicode Release Debug" || "$(CFGREDEFINED)" == "Win32 MT Unicode Release Debug" || "$(CFGREDEFINED)" == "Win32 MTD Unicode Release Debug"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinRelDebU
!ENDIF
LIBSUFFIX=lu$(R_SUFF)
IMPLIBSUFFIX=u$(R_SUFF)
LIBCFG=Win32 MTD Unicode Release Debug
OUTNAME=$(BASENAME)u$(R_SUFF)
CPP_PROJ=/nologo /MD /W3 /GX /YX $(OPTIMIZE) /D "_REL_DEB" $(UNICODE_FLAGS) /D "WIN32" /D "NDEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3  /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c $(AFXDLL) $(DEBUGINFO) $(PDBINFO) $(CFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res $(UNICODE_FLAGS) /d "NDEBUG" /d "_AFXDLL" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
!ENDIF

!IF "$(CFGREDEFINED)" == "Win32 Debug" || "$(CFGREDEFINED)" == "Win32 MT Debug" || "$(CFGREDEFINED)" == "Win32 MTD Debug"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinDebug
!ENDIF
LIBSUFFIX=l$(D_SUFF)
IMPLIBSUFFIX=$(D_SUFF)
LIBCFG=Win32 MTD Debug
#LFLAGS=/DEBUG:FULL $(LFLAGS)
OUTNAME=$(BASENAME)$(D_SUFF)
CPP_PROJ=/nologo /$(MDD) /W3 /GX $(DEBUGINFO) $(PDBINFO) /YX /Od /D "WIN32" /D "_DEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3  /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c $(AFXDLL) $(CFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res /d "_DEBUG" /d "_AFXDLL" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
!ENDIF

!IF "$(CFGREDEFINED)" == "Win32 Unicode Debug" || "$(CFGREDEFINED)" == "Win32 MT Unicode Debug" || "$(CFGREDEFINED)" == "Win32 MTD Unicode Debug"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinDebU
!ENDIF
LIBSUFFIX=lu$(D_SUFF)
IMPLIBSUFFIX=u$(D_SUFF)
LIBCFG=Win32 MTD Unicode Debug
#LFLAGS=/DEBUG:FULL $(LFLAGS)
OUTNAME=$(BASENAME)u$(D_SUFF)
!IF EXIST("unicode.no")
CPP_PROJ=/nologo /$(MDD) /W3 /GX $(DEBUGINFO) $(PDBINFO) /YX /Od /D "WIN32" /D "_DEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3  /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c $(AFXDLL) $(CFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res /d "_DEBUG" /d "_AFXDLL" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
!ELSE
CPP_PROJ=/nologo /$(MDD) /W3 /GX $(DEBUGINFO) $(PDBINFO) /YX /Od $(UNICODE_FLAGS) /D "WIN32" /D "_DEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3  /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c $(AFXDLL) $(CFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res $(UNICODE_FLAGS) /d "_DEBUG" /d "_AFXDLL" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
!ENDIF
!ENDIF

# ========================================================================= MIXED_DLL (Start)

!ELSEIF "$(TARGETTYPE)" == "MIXED_DLL"

!IF "$(OUTDIR)" == "" || "$(OUTDIR)" == "."
OUTDIR=$(PROJPATH)\$(DIR_PREFIX)\DLL$(DIR_SUFFIX)
!ENDIF

!IF "$(OUTEXT)" == ""
OUTEXT=DLL
!ENDIF
SUBSYSTEM=windows /DLL /IMPLIB:$(PROJPATH)/IMPLIB$(DIR_SUFFIX)/$(OUTNAME).LIB

!IF "$(CFGREDEFINED)" == "Win32 Release" || "$(CFGREDEFINED)" == "Win32 MT Release" || "$(CFGREDEFINED)" == "Win32 MTD Release"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinRel
!ENDIF
LIBSUFFIX=l
IMPLIBSUFFIX=
LIBCFG=Win32 MTD Release
OUTNAME=$(BASENAME)
CPP_PROJ=/nologo /MD /W3 /GX $(OPTIMIZE) /D "WIN32" /D "NDEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3 /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c $(AFXDLL) /clr /D_MBCS $(CFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res /d "NDEBUG" /d "_AFXDLL" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
LFLAGS=/NOENTRY /INCLUDE:"__DllMainCRTStartup@12" $(LFLAGS)
!ENDIF

!IF "$(CFGREDEFINED)" == "Win32 Release Debug" || "$(CFGREDEFINED)" == "Win32 MT Release Debug" || "$(CFGREDEFINED)" == "Win32 MTD Release Debug"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinRelDeb
!ENDIF
LIBSUFFIX=l$(R_SUFF)
IMPLIBSUFFIX=$(R_SUFF)
LIBCFG=Win32 MTD Release Debug
OUTNAME=$(BASENAME)$(R_SUFF)
CPP_PROJ=/nologo /MD /W3 /GX $(OPTIMIZE) /D "_REL_DEB" /D "WIN32" /D "NDEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3 /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c $(AFXDLL) $(DEBUGINFO) $(PDBINFO) \
/clr /D_MBCS /Zi /GS $(CFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res /d "NDEBUG" /d "_AFXDLL" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
LFLAGS=/ASSEMBLYDEBUG /FIXED:NO /debug /NOENTRY /INCLUDE:"__DllMainCRTStartup@12" $(LFLAGS)
!ENDIF

!IF "$(CFGREDEFINED)" == "Win32 Unicode Release" || "$(CFGREDEFINED)" == "Win32 MT Unicode Release" || "$(CFGREDEFINED)" == "Win32 MTD Unicode Release"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinRelU
!ENDIF
LIBSUFFIX=lu
IMPLIBSUFFIX=u
LIBCFG=Win32 MTD Unicode Release
OUTNAME=$(BASENAME)u
!IF EXIST("unicode.no")
CPP_PROJ=/nologo /MD /W3 /GX $(OPTIMIZE) /D "WIN32" /D "NDEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3 /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c $(AFXDLL) /clr /D_MBCS $(CFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res /d "NDEBUG" /d "_AFXDLL" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
!ELSE
CPP_PROJ=/nologo /MD /W3 /GX $(OPTIMIZE) $(UNICODE_FLAGS) /D "WIN32" /D "NDEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3 /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c $(AFXDLL) $(CFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res $(UNICODE_FLAGS) /d "NDEBUG" /d "_AFXDLL" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
LFLAGS=/NOENTRY /INCLUDE:"__DllMainCRTStartup@12" $(LFLAGS)
!ENDIF
!ENDIF

!IF "$(CFGREDEFINED)" == "Win32 Unicode Release Debug" || "$(CFGREDEFINED)" == "Win32 MT Unicode Release Debug" || "$(CFGREDEFINED)" == "Win32 MTD Unicode Release Debug"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinRelDebU
!ENDIF
LIBSUFFIX=lu$(R_SUFF)
IMPLIBSUFFIX=u$(R_SUFF)
LIBCFG=Win32 MTD Unicode Release Debug
OUTNAME=$(BASENAME)u$(R_SUFF)
CPP_PROJ=/nologo /MD /W3 /GX $(OPTIMIZE) /D "_REL_DEB" $(UNICODE_FLAGS) /D "WIN32" /D "NDEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3 /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c $(AFXDLL) $(DEBUGINFO) $(PDBINFO) \
/clr /D_MBCS /Zi /GS $(CFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res $(UNICODE_FLAGS) /d "NDEBUG" /d "_AFXDLL" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
LFLAGS=/ASSEMBLYDEBUG /FIXED:NO /debug /NOENTRY /INCLUDE:"__DllMainCRTStartup@12" $(LFLAGS)
!ENDIF

!IF "$(CFGREDEFINED)" == "Win32 Debug" || "$(CFGREDEFINED)" == "Win32 MT Debug" || "$(CFGREDEFINED)" == "Win32 MTD Debug"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinDebug
!ENDIF
LIBSUFFIX=l$(D_SUFF)
IMPLIBSUFFIX=$(D_SUFF)
LIBCFG=Win32 MTD Debug
OUTNAME=$(BASENAME)$(D_SUFF)
CPP_PROJ=/nologo /$(MDD) /W3 /GX /Od /D "WIN32" /D "_DEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3 /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c $(AFXDLL) /clr /D_MBCS /Zi /GS $(CFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res /d "_DEBUG" /d "_AFXDLL" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
LFLAGS=/DEBUG:FULL /ASSEMBLYDEBUG /FIXED:NO /debug /NOENTRY /INCLUDE:"__DllMainCRTStartup@12" $(LFLAGS)
!ENDIF

!IF "$(CFGREDEFINED)" == "Win32 Unicode Debug" || "$(CFGREDEFINED)" == "Win32 MT Unicode Debug" || "$(CFGREDEFINED)" == "Win32 MTD Unicode Debug"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinDebU
!ENDIF
LIBSUFFIX=lu$(D_SUFF)
IMPLIBSUFFIX=u$(D_SUFF)
LIBCFG=Win32 MTD Unicode Debug
OUTNAME=$(BASENAME)u$(D_SUFF)
!IF EXIST("unicode.no")
CPP_PROJ=/nologo /$(MDD) /W3 /GX /Od /D "WIN32" /D "_DEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3 /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c $(AFXDLL) $(CFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res /d "_DEBUG" /d "_AFXDLL" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
!ELSE
CPP_PROJ=/nologo /$(MDD) /W3 /GX /Od $(UNICODE_FLAGS) /D "WIN32" /D "_DEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3 /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c $(AFXDLL) /clr /D_MBCS /Zi /GS $(CFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res $(UNICODE_FLAGS) /d "_DEBUG" /d "_AFXDLL" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
LFLAGS=/DEBUG:FULL /ASSEMBLYDEBUG /FIXED:NO /debug /NOENTRY /INCLUDE:"__DllMainCRTStartup@12" $(LFLAGS)
!ENDIF
!ENDIF

# ========================================================================= MIXED_DLL (End)

# ======================================================================= ATLDLL
!ELSEIF "$(TARGETTYPE)" == "ATLDLL"

!IF "$(OUTDIR)" == "" || "$(OUTDIR)" == "."
OUTDIR=$(PROJPATH)\$(DIR_PREFIX)\DLL$(DIR_SUFFIX)
!ENDIF

!IF "$(OUTEXT)" == ""
OUTEXT=DLL
!ENDIF
SUBSYSTEM=windows /DLL /IMPLIB:$(PROJPATH)/IMPLIB$(DIR_SUFFIX)/$(OUTNAME).LIB

!IF "$(CFGREDEFINED)" == "Win32 Release" || "$(CFGREDEFINED)" == "Win32 MT Release" || "$(CFGREDEFINED)" == "Win32 MTD Release"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinRel
!ENDIF
LIBSUFFIX=l
IMPLIBSUFFIX=
LIBCFG=Win32 MTD Release
OUTNAME=$(BASENAME)
CPP_PROJ=/nologo /MD /W3 /GX /YX $(OPTIMIZE) /D "WIN32" /D "NDEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c \
/D "_MBCS" /D "_USRDLL" $(CFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res /d "NDEBUG" /d "_MBCS" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
!ENDIF

!IF "$(CFGREDEFINED)" == "Win32 Release Debug" || "$(CFGREDEFINED)" == "Win32 MT Release Debug" || "$(CFGREDEFINED)" == "Win32 MTD Release Debug"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinRelDeb
!ENDIF
LIBSUFFIX=l$(R_SUFF)
IMPLIBSUFFIX=$(R_SUFF)
LIBCFG=Win32 MTD Release Debug
OUTNAME=$(BASENAME)$(R_SUFF)
CPP_PROJ=/nologo /MD /W3 /GX /YX $(OPTIMIZE) /D "_REL_DEB" /D "WIN32" /D "NDEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/Fd$(OUTDIR)/$(OUTNAME).PDB /Fo$(INTDIR)/ /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c \
/D "_MBCS" /D "_USRDLL" $(DEBUGINFO) $(PDBINFO) $(CFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res /d "NDEBUG" /d "_MBCS" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
#LFLAGS=/FIXED:NO /debug $(LFLAGS)
!ENDIF

!IF "$(CFGREDEFINED)" == "Win32 Unicode Release" || "$(CFGREDEFINED)" == "Win32 MT Unicode Release" || "$(CFGREDEFINED)" == "Win32 MTD Unicode Release"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinRelU
!ENDIF
LIBSUFFIX=lu
IMPLIBSUFFIX=u
LIBCFG=Win32 MTD Unicode Release
OUTNAME=$(BASENAME)u
!IF EXISTS("unicode.no")
CPP_PROJ=/nologo /MD /W3 /GX /YX $(OPTIMIZE) /D "WIN32" /D "NDEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c \
/D "_MBCS" /D "_USRDLL" $(CFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res /d "NDEBUG" /d "_MBCS" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
!ELSE
CPP_PROJ=/nologo /MD /W3 /GX /YX $(OPTIMIZE) $(UNICODE_FLAGS) /D "WIN32" /D "NDEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c \
/D "_MBCS" /D "_USRDLL" $(CFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res $(UNICODE_FLAGS) /d "NDEBUG" /d "_MBCS" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
!ENDIF
!ENDIF

!IF "$(CFGREDEFINED)" == "Win32 Debug" || "$(CFGREDEFINED)" == "Win32 MT Debug" || "$(CFGREDEFINED)" == "Win32 MTD Debug"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinDebug
!ENDIF
LIBSUFFIX=l$(D_SUFF)
IMPLIBSUFFIX=$(D_SUFF)
LIBCFG=Win32 MTD Debug
#LFLAGS=/DEBUG:FULL $(LFLAGS)
OUTNAME=$(BASENAME)$(D_SUFF)
CPP_PROJ=/nologo /$(MDD) /W3 /GX $(DEBUGINFO) $(PDBINFO) /YX /Od /D "WIN32" /D "_DEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c \
/D "_MBCS" /D "_USRDLL" $(CFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res /d "_DEBUG" /d "_MBCS"  /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
!ENDIF

!IF "$(CFGREDEFINED)" == "Win32 Unicode Debug" || "$(CFGREDEFINED)" == "Win32 MT Unicode Debug" || "$(CFGREDEFINED)" == "Win32 MTD Unicode Debug"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinDebU
#INTDIR=qq
!ENDIF
LIBSUFFIX=lu$(D_SUFF)
IMPLIBSUFFIX=u$(D_SUFF)
LIBCFG=Win32 MTD Unicode Debug
#LFLAGS=/DEBUG:FULL $(LFLAGS)
OUTNAME=$(BASENAME)u$(D_SUFF)
!IF EXIST("unicode.no")
CPP_PROJ=/nologo /$(MDD) /W3 /GX $(DEBUGINFO) $(PDBINFO) /YX /Od /D "WIN32" /D "_DEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c \
/D "_MBCS" /D "_USRDLL" $(CFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res /d "_DEBUG" /d "_MBCS"  /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
!ELSE
CPP_PROJ=/nologo /$(MDD) /W3 /GX $(DEBUGINFO) $(PDBINFO) /YX /Od $(UNICODE_FLAGS) /D "WIN32" /D "_DEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c \
/D "_MBCS" /D "_USRDLL" $(CFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res $(UNICODE_FLAGS) /d "_DEBUG" /d "_MBCS" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
!ENDIF
!ENDIF
# ======================================================================= ATLDLL

!ELSEIF "$(TARGETTYPE)" == "DLL_STATIC"

!IF "$(OUTDIR)" == "" || "$(OUTDIR)" == "."
OUTDIR=$(PROJPATH)\$(DIR_PREFIX)\DLL$(DIR_SUFFIX)
!ENDIF

!IF "$(OUTEXT)" == ""
OUTEXT=DLL
!ENDIF
SUBSYSTEM=windows /DLL /IMPLIB:$(PROJPATH)/IMPLIB$(DIR_SUFFIX)/$(OUTNAME).LIB

!IF "$(CFGREDEFINED)" == "Win32 Release" || "$(CFGREDEFINED)" == "Win32 MT Release" || "$(CFGREDEFINED)" == "Win32 MTD Release"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinRel
!ENDIF
LIBSUFFIX=m
IMPLIBSUFFIX=
LIBCFG=Win32 MT Release
OUTNAME=$(BASENAME)
CPP_PROJ=/nologo /MT /W3 /GX /YX $(OPTIMIZE) /D "WIN32" /D "NDEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3  /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c  $(CFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res /d "NDEBUG" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
!ENDIF

!IF "$(CFGREDEFINED)" == "Win32 Release Debug" || "$(CFGREDEFINED)" == "Win32 MT Release Debug" || "$(CFGREDEFINED)" == "Win32 MTD Release Debug"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinRelDeb
!ENDIF
LIBSUFFIX=m$(R_SUFF)
IMPLIBSUFFIX=$(R_SUFF)
LIBCFG=Win32 MT Release Debug
OUTNAME=$(BASENAME)$(R_SUFF)
CPP_PROJ=/nologo /MT /W3 /GX /YX $(OPTIMIZE) /D "_REL_DEB" /D "WIN32" /D "NDEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3  /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c $(DEBUGINFO) $(PDBINFO) $(CFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res /d "NDEBUG" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
#LFLAGS=/FIXED:NO /debug $(LFLAGS)
!ENDIF

!IF "$(CFGREDEFINED)" == "Win32 Unicode Release" || "$(CFGREDEFINED)" == "Win32 MT Unicode Release" || "$(CFGREDEFINED)" == "Win32 MTD Unicode Release"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinRelU
!ENDIF
LIBSUFFIX=mu
IMPLIBSUFFIX=u
LIBCFG=Win32 MT Unicode Release
OUTNAME=$(BASENAME)u
!IF EXIST("unicode.no")
CPP_PROJ=/nologo /MT /W3 /GX /YX $(OPTIMIZE) /D "WIN32" /D "NDEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3  /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c  $(CFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res /d "NDEBUG" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
!ELSE
CPP_PROJ=/nologo /MT /W3 /GX /YX $(OPTIMIZE) $(UNICODE_FLAGS) /D "WIN32" /D "NDEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3  /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c  $(CFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res $(UNICODE_FLAGS) /d "NDEBUG" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
!ENDIF
!ENDIF

!IF "$(CFGREDEFINED)" == "Win32 Unicode Release Debug" || "$(CFGREDEFINED)" == "Win32 MT Unicode Release Debug" || "$(CFGREDEFINED)" == "Win32 MTD Unicode Release Debug"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinRelDebU
!ENDIF
LIBSUFFIX=mu$(R_SUFF)
IMPLIBSUFFIX=u$(R_SUFF)
LIBCFG=Win32 MT Unicode Release Debug
OUTNAME=$(BASENAME)u$(R_SUFF)
CPP_PROJ=/nologo /MT /W3 /GX /YX $(OPTIMIZE) /D "_REL_DEB" $(UNICODE_FLAGS) /D "WIN32" /D "NDEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3  /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c $(DEBUGINFO) $(PDBINFO) $(CFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res $(UNICODE_FLAGS) /d "NDEBUG" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
#LFLAGS=/FIXED:NO /debug $(LFLAGS)
!ENDIF

!IF "$(CFGREDEFINED)" == "Win32 Debug" || "$(CFGREDEFINED)" == "Win32 MT Debug" || "$(CFGREDEFINED)" == "Win32 MTD Debug"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinDebug
!ENDIF
LIBSUFFIX=m$(D_SUFF)
IMPLIBSUFFIX=$(D_SUFF)
LIBCFG=Win32 MT Debug
#LFLAGS=/DEBUG:FULL $(LFLAGS)
OUTNAME=$(BASENAME)$(D_SUFF)
CPP_PROJ=/nologo /$(MTD) /W3 /GX $(DEBUGINFO) $(PDBINFO) /YX /Od /D "WIN32" /D "_DEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3  /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c  $(CFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res /d "_DEBUG" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
!ENDIF

!IF "$(CFGREDEFINED)" == "Win32 Unicode Debug" || "$(CFGREDEFINED)" == "Win32 MT Unicode Debug" || "$(CFGREDEFINED)" == "Win32 MTD Unicode Debug"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinDebU
!ENDIF
LIBSUFFIX=mu$(D_SUFF)
IMPLIBSUFFIX=u$(D_SUFF)
LIBCFG=Win32 MT Unicode Debug
#LFLAGS=/DEBUG:FULL $(LFLAGS)
OUTNAME=$(BASENAME)u$(D_SUFF)
!IF EXIST("unicode.no")
CPP_PROJ=/nologo /$(MTD) /W3 /GX $(DEBUGINFO) $(PDBINFO) /YX /Od /D "WIN32" /D "_DEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3  /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c  $(CFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res /d "_DEBUG" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
!ELSE
CPP_PROJ=/nologo /$(MTD) /W3 /GX $(DEBUGINFO) $(PDBINFO) /YX /Od $(UNICODE_FLAGS) /D "WIN32" /D "_DEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3  /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c  $(CFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res $(UNICODE_FLAGS) /d "_DEBUG" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
!ENDIF
!ENDIF

# ----------------- Scan DLL -----------------------------------
!ELSEIF "$(TARGETTYPE)" == "DLL_SCAN"

!IF "$(OUTDIR)" == "" || "$(OUTDIR)" == "."
OUTDIR=$(PROJPATH)\$(DIR_PREFIX)\DLL$(DIR_SUFFIX)
!ENDIF

!IF "$(OUTEXT)" == ""
OUTEXT=DLL
!ENDIF
SUBSYSTEM=windows /DLL /IMPLIB:$(PROJPATH)/IMPLIB$(DIR_SUFFIX)/$(OUTNAME).LIB

!IF "$(CFGREDEFINED)" == "Win32 Release" || "$(CFGREDEFINED)" == "Win32 MT Release" || "$(CFGREDEFINED)" == "Win32 MTD Release"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinRel
!ENDIF
LIBSUFFIX=l
IMPLIBSUFFIX=
LIBCFG=Win32 MTD Release
OUTNAME=$(BASENAME)
CPP_PROJ=/nologo /MT /W3 /GX /YX $(OPTIMIZE) /D "WIN32" /D "NDEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3  /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c $(AFXDLL) $(CFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res /d "NDEBUG" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
!ENDIF

!IF "$(CFGREDEFINED)" == "Win32 Release Debug" || "$(CFGREDEFINED)" == "Win32 MT Release Debug" || "$(CFGREDEFINED)" == "Win32 MTD Release Debug"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinRelDeb
!ENDIF
LIBSUFFIX=l$(R_SUFF)
IMPLIBSUFFIX=$(R_SUFF)
LIBCFG=Win32 MTD Release Debug
OUTNAME=$(BASENAME)$(R_SUFF)
CPP_PROJ=/nologo /MT /W3 /GX /YX $(OPTIMIZE) /D "_REL_DEB" /D "WIN32" /D "NDEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3  /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c $(AFXDLL) $(DEBUGINFO) $(PDBINFO) $(CFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res /d "NDEBUG" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
#LFLAGS=/FIXED:NO /debug $(LFLAGS)
!ENDIF

!IF "$(CFGREDEFINED)" == "Win32 Debug" || "$(CFGREDEFINED)" == "Win32 MT Debug" || "$(CFGREDEFINED)" == "Win32 MTD Debug"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinDebug
!ENDIF
LIBSUFFIX=l$(D_SUFF)
IMPLIBSUFFIX=$(D_SUFF)
LIBCFG=Win32 MTD Debug
#LFLAGS=/DEBUG:FULL $(LFLAGS)
OUTNAME=$(BASENAME)$(D_SUFF)
CPP_PROJ=/nologo /$(MDD) /W3 /GX $(DEBUGINFO) $(PDBINFO) /YX /Od /D "WIN32" /D "_DEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3  /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c $(AFXDLL) $(CFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res /d "_DEBUG" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
!ENDIF

!ELSEIF "$(TARGETTYPE)" == "EXE" \
        || "$(TARGETTYPE)" == "MFCEXE" \
        || "$(TARGETTYPE)" == "MFCEXE_STATIC" \
        || "$(TARGETTYPE)" == "ATLEXE" \
        || "$(TARGETTYPE)" == "CMEXE" \
        || "$(TARGETTYPE)" == "CMDEXE" \
        || "$(TARGETTYPE)" == "MIXED_MFCEXE"

!IF "$(OUTDIR)" == "" || "$(OUTDIR)" == "."
OUTDIR=$(PROJPATH)\$(DIR_PREFIX)\BIN$(DIR_SUFFIX)
!ENDIF

!IF "$(OUTEXT)" == ""
OUTEXT=EXE
!ENDIF

!IF "$(TARGETTYPE)" == "EXE"

SUBSYSTEM=console

!IF "$(CFGREDEFINED)" == "Win32 Release" || "$(CFGREDEFINED)" == "Win32 MT Release" || "$(CFGREDEFINED)" == "Win32 MTD Release"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinRel
!ENDIF
LIBSUFFIX=
IMPLIBSUFFIX=
LIBCFG=Win32 Release
OUTNAME=$(BASENAME)
CPP_PROJ=/nologo /W3 /GX /YX $(OPTIMIZE) /D "WIN32" /D "NDEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3  /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c $(CFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res /d "NDEBUG" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
!ENDIF

!IF "$(CFGREDEFINED)" == "Win32 Release Debug" || "$(CFGREDEFINED)" == "Win32 MT Release Debug" || "$(CFGREDEFINED)" == "Win32 MTD Release Debug"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinRelDeb
!ENDIF
LIBSUFFIX=$(R_SUFF)
IMPLIBSUFFIX=$(R_SUFF)
LIBCFG=Win32 Release Debug
OUTNAME=$(BASENAME)$(R_SUFF)
CPP_PROJ=/nologo /W3 /GX /YX $(OPTIMIZE) /D "_REL_DEB" /D "WIN32" /D "NDEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3  /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c $(DEBUGINFO) $(PDBINFO) $(CFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res /d "NDEBUG" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
#LFLAGS=/FIXED:NO /debug $(LFLAGS)
!ENDIF

!IF "$(CFGREDEFINED)" == "Win32 Unicode Release" || "$(CFGREDEFINED)" == "Win32 MT Unicode Release" || "$(CFGREDEFINED)" == "Win32 MTD Unicode Release"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinRelU
!ENDIF
LIBSUFFIX=u
IMPLIBSUFFIX=u
LIBCFG=Win32 Unicode Release
OUTNAME=$(BASENAME)u
!IF EXIST("unicode.no")
CPP_PROJ=/nologo /W3 /GX /YX $(OPTIMIZE) /D "WIN32" /D "NDEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3  /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c $(CFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res /d "NDEBUG" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
!ELSE
CPP_PROJ=/nologo /W3 /GX /YX $(OPTIMIZE) $(UNICODE_FLAGS) /D "WIN32" /D "NDEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3  /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c $(CFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res $(UNICODE_FLAGS) /d "NDEBUG" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
!ENDIF
!ENDIF

!IF "$(CFGREDEFINED)" == "Win32 Unicode Release Debug" || "$(CFGREDEFINED)" == "Win32 MT Unicode Release Debug" || "$(CFGREDEFINED)" == "Win32 MTD Unicode Release Debug"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinRelDebU
!ENDIF
LIBSUFFIX=u$(R_SUFF)
IMPLIBSUFFIX=u$(R_SUFF)
LIBCFG=Win32 Unicode Release Debug
OUTNAME=$(BASENAME)u$(R_SUFF)
CPP_PROJ=/nologo /W3 /GX /YX $(OPTIMIZE) /D "_REL_DEB" $(UNICODE_FLAGS) /D "WIN32" /D "NDEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3  /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c $(DEBUGINFO) $(PDBINFO) $(CFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res $(UNICODE_FLAGS) /d "NDEBUG" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
#LFLAGS=/FIXED:NO /debug $(LFLAGS)
!ENDIF

!IF "$(CFGREDEFINED)" == "Win32 Debug" || "$(CFGREDEFINED)" == "Win32 MT Debug" || "$(CFGREDEFINED)" == "Win32 MTD Debug"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinDebug
!ENDIF
LIBSUFFIX=$(D_SUFF)
IMPLIBSUFFIX=$(D_SUFF)
LIBCFG=Win32 Debug
#LFLAGS=/DEBUG:FULL $(LFLAGS)
OUTNAME=$(BASENAME)$(D_SUFF)
CPP_PROJ=/nologo /W3 /GX $(MLD) $(DEBUGINFO) $(PDBINFO) /YX /Od /D "WIN32" /D "_DEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3  /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c  $(CFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res /d "_DEBUG" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
!ENDIF

!IF "$(CFGREDEFINED)" == "Win32 Unicode Debug" || "$(CFGREDEFINED)" == "Win32 MT Unicode Debug" || "$(CFGREDEFINED)" == "Win32 MTD Unicode Debug"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinDebU
!ENDIF
LIBSUFFIX=u$(D_SUFF)
IMPLIBSUFFIX=u$(D_SUFF)
LIBCFG=Win32 Unicode Debug
#LFLAGS=/DEBUG:FULL $(LFLAGS)
OUTNAME=$(BASENAME)u$(D_SUFF)
!IF EXIST("unicode.no")
CPP_PROJ=/nologo /W3 /GX $(MLD) $(DEBUGINFO) $(PDBINFO) /YX /Od /D "WIN32" /D "_DEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3  /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c  $(CFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res /d "_DEBUG" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
!ELSE
CPP_PROJ=/nologo /W3 /GX $(MLD) $(DEBUGINFO) $(PDBINFO) /YX /Od $(UNICODE_FLAGS) /D "WIN32" /D "_DEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3  /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c  $(CFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res $(UNICODE_FLAGS) /d "_DEBUG" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
!ENDIF
!ENDIF

# =======================================================================================
!ELSEIF "$(TARGETTYPE)" == "CMEXE"

SUBSYSTEM=console

!IF "$(CFGREDEFINED)" == "Win32 Release" || "$(CFGREDEFINED)" == "Win32 MT Release" || "$(CFGREDEFINED)" == "Win32 MTD Release"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinRel
!ENDIF
LIBSUFFIX=m
IMPLIBSUFFIX=
LIBCFG=Win32 MT Release
OUTNAME=$(BASENAME)
CPP_PROJ=/nologo /MT /W3 /GX $(OPTIMIZE) /D "WIN32" /D "NDEBUG" /D "_CONSOLE" /I $(PROJPATH)\UNITS \
/D "_MBCS" /YX /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c $(CFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res /d "NDEBUG" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
!ENDIF

!IF "$(CFGREDEFINED)" == "Win32 Debug" || "$(CFGREDEFINED)" == "Win32 MT Debug" || "$(CFGREDEFINED)" == "Win32 MTD Debug"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinDebug
!ENDIF
LIBSUFFIX=m$(D_SUFF)
IMPLIBSUFFIX=$(D_SUFF)
LIBCFG=Win32 MT Debug
#LFLAGS=/DEBUG:FULL $(LFLAGS)
OUTNAME=$(BASENAME)$(D_SUFF)
CPP_PROJ=/nologo /MTd /W3 /GX $(DEBUGINFO) $(PDBINFO) /Od /D "WIN32" /D "_DEBUG" /D "_CONSOLE" /I $(PROJPATH)\UNITS \
/D "_MBCS" /YX /GZ /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c  $(CFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res /d "_DEBUG" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
!ENDIF

# =======================================================================================
!ELSEIF "$(TARGETTYPE)" == "CMDEXE"

SUBSYSTEM=console

!IF "$(CFGREDEFINED)" == "Win32 Release" || "$(CFGREDEFINED)" == "Win32 MT Release" || "$(CFGREDEFINED)" == "Win32 MTD Release"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinRel
!ENDIF
LIBSUFFIX=l
IMPLIBSUFFIX=
LIBCFG=Win32 MTD Release
OUTNAME=$(BASENAME)
CPP_PROJ=/nologo /MD /W3 /GX $(OPTIMIZE) /D "WIN32" /D "NDEBUG" /D "_CONSOLE" /I $(PROJPATH)\UNITS \
/D "_MBCS" /YX /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c $(CFLAGS)
LFLAGS=/nologo /subsystem:console /incremental:no /machine:I386 $(LFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res /d "NDEBUG" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
!ENDIF

!IF "$(CFGREDEFINED)" == "Win32 Release Debug" || "$(CFGREDEFINED)" == "Win32 MT Release Debug" || "$(CFGREDEFINED)" == "Win32 MTD Release Debug"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinRelDeb
!ENDIF
LIBSUFFIX=l$(R_SUFF)
IMPLIBSUFFIX=$(R_SUFF)
LIBCFG=Win32 MTD Release Debug
OUTNAME=$(BASENAME)$(R_SUFF)
CPP_PROJ=/nologo /MD /W3 /GX $(OPTIMIZE) /D "WIN32" /D "NDEBUG" /D "_CONSOLE" /I $(PROJPATH)\UNITS \
/D "_MBCS" /YX /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c $(DEBUGINFO) $(PDBINFO) $(CFLAGS)
LFLAGS=/nologo /subsystem:console /incremental:no /machine:I386 $(LFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res /d "NDEBUG" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
!ENDIF

!IF "$(CFGREDEFINED)" == "Win32 Debug" || "$(CFGREDEFINED)" == "Win32 MT Debug" || "$(CFGREDEFINED)" == "Win32 MTD Debug"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinDebug
!ENDIF
LIBSUFFIX=l$(D_SUFF)
IMPLIBSUFFIX=$(D_SUFF)
LIBCFG=Win32 MTD Debug
OUTNAME=$(BASENAME)$(D_SUFF)
CPP_PROJ=/nologo /MDd /W3 /GX $(DEBUGINFO) $(PDBINFO) /Od /D "WIN32" /D "_DEBUG" /D "_CONSOLE" /I $(PROJPATH)\UNITS \
/D "_MBCS" /YX /GZ /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c  $(CFLAGS)
LFLAGS=/DEBUG:FULL /nologo /subsystem:console /incremental:no /debug /machine:I386 $(LFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res /d "_DEBUG" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
!ENDIF

!IF "$(CFGREDEFINED)" == "Win32 Unicode Release" || "$(CFGREDEFINED)" == "Win32 MT Unicode Release" || "$(CFGREDEFINED)" == "Win32 MTD Unicode Release"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinRelU
!ENDIF
LIBSUFFIX=lu
IMPLIBSUFFIX=u
LIBCFG=Win32 MTD Unicode Release
OUTNAME=$(BASENAME)u
!IF EXIST("unicode.no")
CPP_PROJ=/nologo /MD /W3 /GX $(OPTIMIZE) /D "WIN32" /D "NDEBUG" /D "_CONSOLE" /I $(PROJPATH)\UNITS \
/D "_MBCS" /YX /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c $(CFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res /d "NDEBUG" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
!ELSE
CPP_PROJ=/nologo /MD /W3 /GX $(OPTIMIZE) $(UNICODE_FLAGS) /D "WIN32" /D "NDEBUG" /D "_CONSOLE" /I $(PROJPATH)\UNITS \
/YX /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c $(CFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res $(UNICODE_FLAGS) /d "NDEBUG" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
!ENDIF
LFLAGS=/nologo /subsystem:console /incremental:no /machine:I386 $(LFLAGS)
!ENDIF

!IF "$(CFGREDEFINED)" == "Win32 Unicode Release Debug" || "$(CFGREDEFINED)" == "Win32 MT Unicode Release Debug" || "$(CFGREDEFINED)" == "Win32 MTD Unicode Release Debug"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinRelDebU
!ENDIF
LIBSUFFIX=lu$(R_SUFF)
IMPLIBSUFFIX=u$(R_SUFF)
LIBCFG=Win32 MTD Unicode Release Debug
OUTNAME=$(BASENAME)u$(R_SUFF)
CPP_PROJ=/nologo /MD /W3 /GX $(OPTIMIZE) $(UNICODE_FLAGS) /D "WIN32" /D "NDEBUG" /D "_CONSOLE" /I $(PROJPATH)\UNITS \
/YX /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c $(DEBUGINFO) $(PDBINFO) $(CFLAGS)
LFLAGS=/nologo /subsystem:console /incremental:no /machine:I386 $(LFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res $(UNICODE_FLAGS) /d "NDEBUG" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
!ENDIF

!IF "$(CFGREDEFINED)" == "Win32 Unicode Debug" || "$(CFGREDEFINED)" == "Win32 MT Unicode Debug" || "$(CFGREDEFINED)" == "Win32 MTD Unicode Debug"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinDebU
!ENDIF
LIBSUFFIX=lu$(D_SUFF)
IMPLIBSUFFIX=u$(D_SUFF)
LIBCFG=Win32 MTD Unicode Debug
OUTNAME=$(BASENAME)u$(D_SUFF)
!IF EXIST("unicode.no")
CPP_PROJ=/nologo /MDd /W3 /GX $(DEBUGINFO) $(PDBINFO) /Od /D "WIN32" /D "_DEBUG" /D "_CONSOLE" /I $(PROJPATH)\UNITS \
/D "_MBCS" /YX /GZ /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c  $(CFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res /d "_DEBUG" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
!ELSE
CPP_PROJ=/nologo /MDd /W3 /GX $(DEBUGINFO) $(PDBINFO) /Od $(UNICODE_FLAGS) /D "WIN32" /D "_DEBUG" /D "_CONSOLE" /I $(PROJPATH)\UNITS \
/YX /GZ /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c  $(CFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res $(UNICODE_FLAGS) /d "_DEBUG" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
!ENDIF
LFLAGS=/DEBUG:FULL /nologo /subsystem:console /incremental:no /debug /machine:I386 $(LFLAGS)
!ENDIF

# =======================================================================================

!ELSEIF "$(TARGETTYPE)" == "MFCEXE_STATIC"

SUBSYSTEM=windows

!IF "$(CFGREDEFINED)" == "Win32 Release" || "$(CFGREDEFINED)" == "Win32 MT Release" || "$(CFGREDEFINED)" == "Win32 MTD Release"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinRel
!ENDIF
LIBSUFFIX=m
IMPLIBSUFFIX=
LIBCFG=Win32 MT Release
OUTNAME=$(BASENAME)
CPP_PROJ=/nologo /MT /W3 /GX /YX $(OPTIMIZE) /D "WIN32" /D "NDEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3  /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c $(CFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res /d "NDEBUG" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
!ENDIF

!IF "$(CFGREDEFINED)" == "Win32 Release Debug" || "$(CFGREDEFINED)" == "Win32 MT Release Debug" || "$(CFGREDEFINED)" == "Win32 MTD Release Debug"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinRelDeb
!ENDIF
LIBSUFFIX=m$(R_SUFF)
IMPLIBSUFFIX=$(R_SUFF)
LIBCFG=Win32 MT Release Debug
OUTNAME=$(BASENAME)$(R_SUFF)
CPP_PROJ=/nologo /MT /W3 /GX /YX $(OPTIMIZE) /D "_REL_DEB" /D "WIN32" /D "NDEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3  /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c $(DEBUGINFO) $(PDBINFO) $(CFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res /d "NDEBUG" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
#LFLAGS=/FIXED:NO /debug $(LFLAGS)
!ENDIF

!IF "$(CFGREDEFINED)" == "Win32 Unicode Release" || "$(CFGREDEFINED)" == "Win32 MT Unicode Release" || "$(CFGREDEFINED)" == "Win32 MTD Unicode Release"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinRelU
!ENDIF
LIBSUFFIX=mu
IMPLIBSUFFIX=u
LIBCFG=Win32 MT Unicode Release
OUTNAME=$(BASENAME)
!IF EXIST("unicode.no")
CPP_PROJ=/nologo /MT /W3 /GX /YX $(OPTIMIZE) /D "WIN32" /D "NDEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3  /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c $(CFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res /d "NDEBUG" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
!ELSE
CPP_PROJ=/nologo /MT /W3 /GX /YX $(OPTIMIZE) $(UNICODE_FLAGS) /D "WIN32" /D "NDEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3  /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c $(CFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res $(UNICODE_FLAGS) /d "NDEBUG" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
!ENDIF
!ENDIF

!IF "$(CFGREDEFINED)" == "Win32 Unicode Release Debug" || "$(CFGREDEFINED)" == "Win32 MT Unicode Release Debug" || "$(CFGREDEFINED)" == "Win32 MTD Unicode Release Debug"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinRelDebU
!ENDIF
LIBSUFFIX=mu$(R_SUFF)
IMPLIBSUFFIX=u$(R_SUFF)
LIBCFG=Win32 MT Unicode Release Debug
OUTNAME=$(BASENAME)$(R_SUFF)
CPP_PROJ=/nologo /MT /W3 /GX /YX $(OPTIMIZE) /D "_REL_DEB" $(UNICODE_FLAGS) /D "WIN32" /D "NDEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3  /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c $(DEBUGINFO) $(PDBINFO) $(CFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res $(UNICODE_FLAGS) /d "NDEBUG" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
#LFLAGS=/FIXED:NO /debug $(LFLAGS)
!ENDIF

!IF "$(CFGREDEFINED)" == "Win32 Debug" || "$(CFGREDEFINED)" == "Win32 MT Debug" || "$(CFGREDEFINED)" == "Win32 MTD Debug"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinDebug
!ENDIF
LIBSUFFIX=m$(D_SUFF)
IMPLIBSUFFIX=$(D_SUFF)
LIBCFG=Win32 MT Debug
#LFLAGS=/DEBUG:FULL $(LFLAGS)
OUTNAME=$(BASENAME)$(D_SUFF)
CPP_PROJ=/nologo /$(MTD) /W3 /GX $(DEBUGINFO) $(PDBINFO) /YX /Od /D "WIN32" /D "_DEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3  /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c $(CFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res /d "_DEBUG" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
!ENDIF

!IF "$(CFGREDEFINED)" == "Win32 Unicode Debug" || "$(CFGREDEFINED)" == "Win32 MT Unicode Debug" || "$(CFGREDEFINED)" == "Win32 MTD Unicode Debug"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinDebU
!ENDIF
LIBSUFFIX=mu$(D_SUFF)
IMPLIBSUFFIX=u$(D_SUFF)
LIBCFG=Win32 MT Unicode Debug
#LFLAGS=/DEBUG:FULL $(LFLAGS)
OUTNAME=$(BASENAME)u$(D_SUFF)
!IF EXIST("unicode.no")
CPP_PROJ=/nologo /$(MTD) /W3 /GX $(DEBUGINFO) $(PDBINFO) /YX /Od /D "WIN32" /D "_DEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3  /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c $(CFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res /d "_DEBUG" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
!ELSE
CPP_PROJ=/nologo /$(MTD) /W3 /GX $(DEBUGINFO) $(PDBINFO) /YX /Od $(UNICODE_FLAGS) /D "WIN32" /D "_DEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3  /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c $(CFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res $(UNICODE_FLAGS) /d "_DEBUG" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
!ENDIF
!ENDIF

!ELSEIF "$(TARGETTYPE)" == "ATLEXE"

SUBSYSTEM=windows

!IF "$(CFGREDEFINED)" == "Win32 Release" || "$(CFGREDEFINED)" == "Win32 MT Release" || "$(CFGREDEFINED)" == "Win32 MTD Release"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinRel
!ENDIF
LIBSUFFIX=l
IMPLIBSUFFIX=
LIBCFG=Win32 MTD Release
OUTNAME=$(BASENAME)
CPP_PROJ=/nologo /MD /W3 /GX $(OPTIMIZE) /YX /D "WIN32" /D "NDEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/D "_MBCS" /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo"$(INTDIR)/" /c $(CFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res /d "NDEBUG" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
!ENDIF

!IF "$(CFGREDEFINED)" == "Win32 Release Debug" || "$(CFGREDEFINED)" == "Win32 MT Release Debug" || "$(CFGREDEFINED)" == "Win32 MTD Release Debug"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinRelDeb
!ENDIF
LIBSUFFIX=l$(R_SUFF)
IMPLIBSUFFIX=$(R_SUFF)
LIBCFG=Win32 MTD Release Debug
OUTNAME=$(BASENAME)$(R_SUFF)
CPP_PROJ=/nologo /MD /W3 /GX $(OPTIMIZE) /YX /D "_REL_DEB" /D "WIN32" /D "NDEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/D "_MBCS" /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo"$(INTDIR)/" /c $(DEBUGINFO) $(PDBINFO) $(CFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res /d "NDEBUG" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
#LFLAGS=/FIXED:NO /debug $(LFLAGS)
!ENDIF

!IF "$(CFGREDEFINED)" == "Win32 Unicode Release" || "$(CFGREDEFINED)" == "Win32 MT Unicode Release" || "$(CFGREDEFINED)" == "Win32 MTD Unicode Release"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinRelU
!ENDIF
LIBSUFFIX=lu
IMPLIBSUFFIX=u
LIBCFG=Win32 MTD Unicode Release
OUTNAME=$(BASENAME)u
!IF EXIST("unicode.no")
CPP_PROJ=/nologo /MD /W3 /GX $(OPTIMIZE) /YX /D "WIN32" /D "NDEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/Fp$(OUTDIR)/$(OUTNAME).PCH /Fo"$(INTDIR)/" /c $(CFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res /d "NDEBUG" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
!ELSE
CPP_PROJ=/nologo /MD /W3 /GX $(OPTIMIZE) /YX $(UNICODE_FLAGS) /D "WIN32" /D "NDEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/Fp$(OUTDIR)/$(OUTNAME).PCH /Fo"$(INTDIR)/" /c $(CFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res $(UNICODE_FLAGS) /d "NDEBUG" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
!ENDIF
!ENDIF

!IF "$(CFGREDEFINED)" == "Win32 Unicode Release Debug" || "$(CFGREDEFINED)" == "Win32 MT Unicode Release Debug" || "$(CFGREDEFINED)" == "Win32 MTD Unicode Release Debug"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinRelDebU
!ENDIF
LIBSUFFIX=lu$(R_SUFF)
IMPLIBSUFFIX=u$(R_SUFF)
LIBCFG=Win32 MTD Unicode Release Debug
OUTNAME=$(BASENAME)u$(R_SUFF)
CPP_PROJ=/nologo /MD /W3 /GX $(OPTIMIZE) /YX /D "_REL_DEB" $(UNICODE_FLAGS) /D "WIN32" /D "NDEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/Fp$(OUTDIR)/$(OUTNAME).PCH /Fo"$(INTDIR)/" /c $(DEBUGINFO) $(PDBINFO) $(CFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res $(UNICODE_FLAGS) /d "NDEBUG" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
#LFLAGS=/FIXED:NO /debug $(LFLAGS)
!ENDIF

!IF "$(CFGREDEFINED)" == "Win32 Debug" || "$(CFGREDEFINED)" == "Win32 MT Debug" || "$(CFGREDEFINED)" == "Win32 MTD Debug"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinDebug
!ENDIF
LIBSUFFIX=l$(D_SUFF)
IMPLIBSUFFIX=$(D_SUFF)
LIBCFG=Win32 MTD Debug
#LFLAGS=/DEBUG:FULL $(LFLAGS)
OUTNAME=$(BASENAME)$(D_SUFF)
CPP_PROJ=/nologo /MDd /W3 /GX $(DEBUGINFO) $(PDBINFO) /Od /YX /D "WIN32" /D "_DEBUG" /D "_WINDOWS" /D "_MBCS" /I $(PROJPATH)\UNITS \
/Fp$(OUTDIR)/$(OUTNAME).PCH /Fo"$(INTDIR)/" /GZ /c $(CFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res /d "_DEBUG" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
!ENDIF

!IF "$(CFGREDEFINED)" == "Win32 Unicode Debug" || "$(CFGREDEFINED)" == "Win32 MT Unicode Debug" || "$(CFGREDEFINED)" == "Win32 MTD Unicode Debug"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinDebU
!ENDIF
LIBSUFFIX=lu$(D_SUFF)
IMPLIBSUFFIX=u$(D_SUFF)
LIBCFG=Win32 MTD Unicode Debug
#LFLAGS=/DEBUG:FULL $(LFLAGS)
OUTNAME=$(BASENAME)u$(D_SUFF)
!IF EXIST("unicode.no")
CPP_PROJ=/nologo /MDd /W3 /GX $(DEBUGINFO) $(PDBINFO) /Od /YX /D "WIN32" /D "_DEBUG" /D "_WINDOWS" /D "_MBCS" /I $(PROJPATH)\UNITS \
/Fp$(OUTDIR)/$(OUTNAME).PCH /Fo"$(INTDIR)/" /GZ /c $(CFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res /d "_DEBUG" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
!ELSE
CPP_PROJ=/nologo /MDd /W3 /GX $(DEBUGINFO) $(PDBINFO) /Od /YX $(UNICODE_FLAGS) /D "WIN32" /D "_DEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/Fp$(OUTDIR)/$(OUTNAME).PCH /Fo"$(INTDIR)/" /GZ /c $(CFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res $(UNICODE_FLAGS) /d "_DEBUG" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
!ENDIF
!ENDIF

# ==================================================================== MIXED_MFCEXE Begin
!ELSEIF "$(TARGETTYPE)" == "MIXED_MFCEXE"

SUBSYSTEM=windows

!IF "$(CFGREDEFINED)" == "Win32 Release" || "$(CFGREDEFINED)" == "Win32 MT Release" || "$(CFGREDEFINED)" == "Win32 MTD Release"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinRel
!ENDIF
LIBSUFFIX=l
IMPLIBSUFFIX=
LIBCFG=Win32 MTD Release
OUTNAME=$(BASENAME)
CPP_PROJ=/nologo /MD /W3 /GX $(OPTIMIZE) /D "WIN32" /D "NDEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3 /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c /D "_AFXDLL" \
/clr /D _MBCS $(CFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res /d "NDEBUG" /d "_AFXDLL" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
!ENDIF

!IF "$(CFGREDEFINED)" == "Win32 Release Debug" || "$(CFGREDEFINED)" == "Win32 MT Release Debug" || "$(CFGREDEFINED)" == "Win32 MTD Release Debug"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinRelDeb
!ENDIF
LIBSUFFIX=l$(R_SUFF)
IMPLIBSUFFIX=$(R_SUFF)
LIBCFG=Win32 MTD Release Debug
OUTNAME=$(BASENAME)$(R_SUFF)
CPP_PROJ=/nologo /MD /W3 /GX $(OPTIMIZE) /D "_REL_DEB" /D "WIN32" /D "NDEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3 /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c /D "_AFXDLL" \
/clr /D _MBCS /GS $(DEBUGINFO) $(PDBINFO) $(CFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res /d "NDEBUG" /d "_AFXDLL" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
LFLAGS=/ASSEMBLYDEBUG /FIXED:NO /debug $(LFLAGS)
!ENDIF

!IF "$(CFGREDEFINED)" == "Win32 Unicode Release" || "$(CFGREDEFINED)" == "Win32 MT Unicode Release" || "$(CFGREDEFINED)" == "Win32 MTD Unicode Release"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinRelU
!ENDIF
LIBSUFFIX=lu
IMPLIBSUFFIX=u
LIBCFG=Win32 MTD Unicode Release
OUTNAME=$(BASENAME)u
!IF EXIST("unicode.no")
CPP_PROJ=/nologo /MD /W3 /GX $(OPTIMIZE) /D "WIN32" /D "NDEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3 /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c /D "_AFXDLL" \
/clr /D _MBCS $(CFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res /d "NDEBUG" /d "_AFXDLL" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
!ELSE
CPP_PROJ=/nologo /MD /W3 /GX $(OPTIMIZE) $(UNICODE_FLAGS) /D "WIN32" /D "NDEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3 /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c /D "_AFXDLL" \
/clr /D _MBCS $(CFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res $(UNICODE_FLAGS) /d "NDEBUG" /d "_AFXDLL" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
!ENDIF
!ENDIF

!IF "$(CFGREDEFINED)" == "Win32 Unicode Release Debug" || "$(CFGREDEFINED)" == "Win32 MT Unicode Release Debug" || "$(CFGREDEFINED)" == "Win32 MTD Unicode Release Debug"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinRelDebU
!ENDIF
LIBSUFFIX=lu$(R_SUFF)
IMPLIBSUFFIX=u$(R_SUFF)
LIBCFG=Win32 MTD Unicode Release Debug
OUTNAME=$(BASENAME)u$(R_SUFF)
CPP_PROJ=/nologo /MD /W3 /GX $(OPTIMIZE) /D "_REL_DEB" $(UNICODE_FLAGS) /D "WIN32" /D "NDEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3 /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c /D "_AFXDLL" \
/clr /D _MBCS /GS $(DEBUGINFO) $(PDBINFO) $(CFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res $(UNICODE_FLAGS) /d "NDEBUG" /d "_AFXDLL" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
LFLAGS=/ASSEMBLYDEBUG /FIXED:NO /debug $(LFLAGS)
!ENDIF

!IF "$(CFGREDEFINED)" == "Win32 Debug" || "$(CFGREDEFINED)" == "Win32 MT Debug" || "$(CFGREDEFINED)" == "Win32 MTD Debug"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinDebug
!ENDIF
LIBSUFFIX=l$(D_SUFF)
IMPLIBSUFFIX=$(D_SUFF)
LIBCFG=Win32 MTD Debug
OUTNAME=$(BASENAME)$(D_SUFF)
CPP_PROJ=/nologo /$(MDD) /W3 /GX /Od /D "WIN32" /D "_DEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3 /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c /D "_AFXDLL" \
/clr /D _MBCS /Zi /GS $(CFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res /d "_DEBUG" /d "_AFXDLL" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
LFLAGS=/DEBUG:FULL /ASSEMBLYDEBUG /FIXED:NO /debug $(LFLAGS)
!ENDIF

!IF "$(CFGREDEFINED)" == "Win32 Unicode Debug" || "$(CFGREDEFINED)" == "Win32 MT Unicode Debug" || "$(CFGREDEFINED)" == "Win32 MTD Unicode Debug"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinDebU
!ENDIF
LIBSUFFIX=lu$(D_SUFF)
IMPLIBSUFFIX=u$(D_SUFF)
LIBCFG=Win32 MTD Unicode Debug
OUTNAME=$(BASENAME)u$(D_SUFF)
!IF EXIST("unicode.no")
CPP_PROJ=/nologo /$(MDD) /W3 /GX /Od /D "WIN32" /D "_DEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3 /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c /D "_AFXDLL" \
/clr /D _MBCS /Zi /GS $(CFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res /d "_DEBUG" /d "_AFXDLL" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
!ELSE
CPP_PROJ=/nologo /$(MDD) /W3 /GX /Od $(UNICODE_FLAGS) /D "WIN32" /D "_DEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3 /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c /D "_AFXDLL" \
/clr /D _MBCS /Zi /GS $(CFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res $(UNICODE_FLAGS) /d "_DEBUG" /d "_AFXDLL" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
!ENDIF
LFLAGS=/DEBUG:FULL /ASSEMBLYDEBUG /FIXED:NO /debug $(LFLAGS)
!ENDIF

# ==================================================================== MIXED_MFCEXE End

!ELSE   #MFCEXE

SUBSYSTEM=windows

!IF "$(CFGREDEFINED)" == "Win32 Release" || "$(CFGREDEFINED)" == "Win32 MT Release" || "$(CFGREDEFINED)" == "Win32 MTD Release"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinRel
!ENDIF
LIBSUFFIX=l
IMPLIBSUFFIX=
LIBCFG=Win32 MTD Release
OUTNAME=$(BASENAME)
CPP_PROJ=/nologo /MD /W3 /GX /YX $(OPTIMIZE) /D "WIN32" /D "NDEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3  /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c /D "_AFXDLL" $(CFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res /d "NDEBUG" /d "_AFXDLL" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
!ENDIF

!IF "$(CFGREDEFINED)" == "Win32 Release Debug" || "$(CFGREDEFINED)" == "Win32 MT Release Debug" || "$(CFGREDEFINED)" == "Win32 MTD Release Debug"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinRelDeb
!ENDIF
LIBSUFFIX=l$(R_SUFF)
IMPLIBSUFFIX=$(R_SUFF)
LIBCFG=Win32 MTD Release Debug
OUTNAME=$(BASENAME)$(R_SUFF)
CPP_PROJ=/nologo /MD /W3 /GX /YX $(OPTIMIZE) /D "_REL_DEB" /D "WIN32" /D "NDEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3  /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c /D "_AFXDLL" $(DEBUGINFO) $(PDBINFO) $(CFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res /d "NDEBUG" /d "_AFXDLL" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
#LFLAGS=FIXED:NO /debug $(LFLAGS)
!ENDIF

!IF "$(CFGREDEFINED)" == "Win32 Unicode Release" || "$(CFGREDEFINED)" == "Win32 MT Unicode Release" || "$(CFGREDEFINED)" == "Win32 MTD Unicode Release"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinRelU
!ENDIF
LIBSUFFIX=lu
IMPLIBSUFFIX=u
LIBCFG=Win32 MTD Unicode Release
OUTNAME=$(BASENAME)u
!IF EXIST("unicode.no")
CPP_PROJ=/nologo /MD /W3 /GX /YX $(OPTIMIZE) /D "WIN32" /D "NDEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3  /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c /D "_AFXDLL" $(CFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res /d "NDEBUG" /d "_AFXDLL" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
!ELSE
CPP_PROJ=/nologo /MD /W3 /GX /YX $(OPTIMIZE) $(UNICODE_FLAGS) /D "WIN32" /D "NDEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3  /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c /D "_AFXDLL" $(CFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res $(UNICODE_FLAGS) /d "NDEBUG" /d "_AFXDLL" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
!ENDIF
!ENDIF

!IF "$(CFGREDEFINED)" == "Win32 Unicode Release Debug" || "$(CFGREDEFINED)" == "Win32 MT Unicode Release Debug" || "$(CFGREDEFINED)" == "Win32 MTD Unicode Release Debug"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinRelDebU
!ENDIF
LIBSUFFIX=lu$(R_SUFF)
IMPLIBSUFFIX=u$(R_SUFF)
LIBCFG=Win32 MTD Unicode Release Debug
OUTNAME=$(BASENAME)u$(R_SUFF)
CPP_PROJ=/nologo /MD /W3 /GX /YX $(OPTIMIZE) /D "_REL_DEB" $(UNICODE_FLAGS) /D "WIN32" /D "NDEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3  /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c /D "_AFXDLL" $(DEBUGINFO) $(PDBINFO) $(CFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res $(UNICODE_FLAGS) /d "NDEBUG" /d "_AFXDLL" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
#LFLAGS=/FIXED:NO /debug $(LFLAGS)
!ENDIF

!IF "$(CFGREDEFINED)" == "Win32 Debug" || "$(CFGREDEFINED)" == "Win32 MT Debug" || "$(CFGREDEFINED)" == "Win32 MTD Debug"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinDebug
!ENDIF
LIBSUFFIX=l$(D_SUFF)
IMPLIBSUFFIX=$(D_SUFF)
LIBCFG=Win32 MTD Debug
#LFLAGS=/DEBUG:FULL $(LFLAGS)
OUTNAME=$(BASENAME)$(D_SUFF)
CPP_PROJ=/nologo /$(MDD) /W3 /GX $(DEBUGINFO) $(PDBINFO) /YX /Od /D "WIN32" /D "_DEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3  /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c /D "_AFXDLL" $(CFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res /d "_DEBUG" /d "_AFXDLL" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
!ENDIF

!IF "$(CFGREDEFINED)" == "Win32 Unicode Debug" || "$(CFGREDEFINED)" == "Win32 MT Unicode Debug" || "$(CFGREDEFINED)" == "Win32 MTD Unicode Debug"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinDebU
!ENDIF
LIBSUFFIX=lu$(D_SUFF)
IMPLIBSUFFIX=u$(D_SUFF)
LIBCFG=Win32 MTD Unicode Debug
#LFLAGS=/DEBUG:FULL $(LFLAGS)
OUTNAME=$(BASENAME)u$(D_SUFF)
!IF EXIST("unicode.no")
CPP_PROJ=/nologo /$(MDD) /W3 /GX $(DEBUGINFO) $(PDBINFO) /YX /Od /D "WIN32" /D "_DEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3  /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c /D "_AFXDLL" $(CFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res /d "_DEBUG" /d "_AFXDLL" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
!ELSE
CPP_PROJ=/nologo /$(MDD) /W3 /GX $(DEBUGINFO) $(PDBINFO) /YX /Od $(UNICODE_FLAGS) /D "WIN32" /D "_DEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3  /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c /D "_AFXDLL" $(CFLAGS)
RSC_PROJ=/l 0x409 /fo$(INTDIR)/$(*B).res $(UNICODE_FLAGS) /d "_DEBUG" /d "_AFXDLL" /i $(PROJPATH) /I $(PROJPATH)\UNITS $(RFLAGS)
!ENDIF
!ENDIF

!ENDIF

!ELSEIF "$(TARGETTYPE)" == "LIB"

LIBCFG=$(CFGREDEFINED)

!IF "$(OUTDIR)" == "" || "$(OUTDIR)" == "."
OUTDIR=$(PROJPATH)\$(DIR_PREFIX)\LIB$(DIR_SUFFIX)
!ENDIF

!IF "$(OUTEXT)" == ""
OUTEXT=LIB
!ENDIF

!IF  "$(CFGREDEFINED)" == "Win32 Release"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinRel
!ENDIF
OUTNAME=$(BASENAME)
CPP_PROJ=/nologo /W3 /GX /YX $(OPTIMIZE) /D "WIN32" /D "NDEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3 /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c $(CFLAGS)
!ELSEIF  "$(CFGREDEFINED)" == "Win32 Release Debug"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinRelDeb
!ENDIF
OUTNAME=$(BASENAME)$(R_SUFF)
CPP_PROJ=/nologo /W3 /GX /YX $(OPTIMIZE) /D "_REL_DEB" /D "WIN32" /D "NDEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3 /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c $(DEBUGINFO) $(PDBINFO) $(CFLAGS)
!ELSEIF "$(CFGREDEFINED)" == "Win32 MT Release"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinRelMT
!ENDIF
OUTNAME=$(BASENAME)m
CPP_PROJ=/nologo /MT /W3 /GX /YX $(OPTIMIZE) /D "WIN32" /D "NDEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3 /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c $(CFLAGS)
!ELSEIF "$(CFGREDEFINED)" == "Win32 MT Release Debug"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinRelDebMT
!ENDIF
OUTNAME=$(BASENAME)m$(R_SUFF)
CPP_PROJ=/nologo /MT /W3 /GX /YX $(OPTIMIZE) /D "_REL_DEB" /D "WIN32" /D "NDEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3 /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c $(DEBUGINFO) $(PDBINFO) $(CFLAGS)
!ELSEIF "$(CFGREDEFINED)" == "Win32 MTD Release"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinRelMD
!ENDIF
OUTNAME=$(BASENAME)l
CPP_PROJ=/nologo /MD /W3 /GX /YX $(OPTIMIZE) /D "WIN32" /D "NDEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3 /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ \
/c $(AFXDLL) $(CFLAGS)
!ELSEIF "$(CFGREDEFINED)" == "Win32 MTD Release Debug"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinRelDebMD
!ENDIF
OUTNAME=$(BASENAME)l$(R_SUFF)
CPP_PROJ=/nologo /MD /W3 /GX /YX $(OPTIMIZE) /D "_REL_DEB" /D "WIN32" /D "NDEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3 /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ \
/c $(AFXDLL) $(DEBUGINFO) $(PDBINFO) $(CFLAGS)
!ELSEIF  "$(CFGREDEFINED)" == "Win32 Debug"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinDebug
!ENDIF
OUTNAME=$(BASENAME)$(D_SUFF)
CPP_PROJ=/nologo /W3 /GX $(MLD) $(DEBUGINFO) $(PDBINFO) /YX /Od /D "WIN32" /D "_DEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3 /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c $(CFLAGS)
!ELSEIF  "$(CFGREDEFINED)" == "Win32 MT Debug"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinDebMT
!ENDIF
OUTNAME=$(BASENAME)m$(D_SUFF)
CPP_PROJ=/nologo /$(MTD) /W3 /GX $(DEBUGINFO) $(PDBINFO) /YX /Od /D "WIN32" /D "_DEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3 /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c $(CFLAGS)
!ELSEIF  "$(CFGREDEFINED)" == "Win32 MTD Debug"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinDebMD
!ENDIF
OUTNAME=$(BASENAME)l$(D_SUFF)
CPP_PROJ=/nologo /$(MDD) /W3 /GX $(DEBUGINFO) $(PDBINFO) /YX /Od /D "WIN32" /D "_DEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3 /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ \
/c $(AFXDLL) $(CFLAGS)
!ENDIF

!IF  "$(CFGREDEFINED)" == "Win32 Unicode Release"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinRelU
!ENDIF
OUTNAME=$(BASENAME)u
!IF EXIST("unicode.no")
CPP_PROJ=/nologo /W3 /GX /YX $(OPTIMIZE) /D "WIN32" /D "NDEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3 /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c $(CFLAGS)
!ELSE
CPP_PROJ=/nologo /W3 /GX /YX $(OPTIMIZE) $(UNICODE_FLAGS) /D "WIN32" /D "NDEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3 /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c $(CFLAGS)
!ENDIF
!ELSEIF  "$(CFGREDEFINED)" == "Win32 Unicode Release Debug"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinRelDebU
!ENDIF
OUTNAME=$(BASENAME)u$(R_SUFF)
CPP_PROJ=/nologo /W3 /GX /YX $(OPTIMIZE) /D "_REL_DEB" $(UNICODE_FLAGS) /D "WIN32" /D "NDEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3 /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c $(DEBUGINFO) $(PDBINFO) $(CFLAGS)
!ELSEIF "$(CFGREDEFINED)" == "Win32 MT Unicode Release"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinRelMTU
!ENDIF
OUTNAME=$(BASENAME)mu
!IF EXIST("unicode.no")
CPP_PROJ=/nologo /MT /W3 /GX /YX $(OPTIMIZE) /D "WIN32" /D "NDEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3 /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c $(CFLAGS)
!ELSE
CPP_PROJ=/nologo /MT /W3 /GX /YX $(OPTIMIZE) $(UNICODE_FLAGS) /D "WIN32" /D "NDEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3 /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c $(CFLAGS)
!ENDIF
!ELSEIF "$(CFGREDEFINED)" == "Win32 MT Unicode Release Debug"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinRelDebMTU
!ENDIF
OUTNAME=$(BASENAME)mu$(R_SUFF)
CPP_PROJ=/nologo /MT /W3 /GX /YX $(OPTIMIZE) /D "_REL_DEB" $(UNICODE_FLAGS) /D "WIN32" /D "NDEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3 /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c $(DEBUGINFO) $(PDBINFO) $(CFLAGS)

!ELSEIF "$(CFGREDEFINED)" == "Win32 MTD Unicode Release"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinRelMDU
!ENDIF
OUTNAME=$(BASENAME)lu
!IF EXIST("unicode.no")
CPP_PROJ=/nologo /MD /W3 /GX /YX $(OPTIMIZE) /D "WIN32" /D "NDEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3 /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ \
/c $(AFXDLL) $(CFLAGS)
!ELSE # !IF EXIST("unicode.no")
CPP_PROJ=/nologo /MD /W3 /GX /YX $(OPTIMIZE) $(UNICODE_FLAGS) /D "WIN32" /D "NDEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3 /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ \
/c $(AFXDLL) $(CFLAGS)
!ENDIF # !IF EXIST("unicode.no")

!ELSEIF "$(CFGREDEFINED)" == "Win32 MTD Unicode Release Debug"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinRelDebMDU
!ENDIF
OUTNAME=$(BASENAME)lu$(R_SUFF)
CPP_PROJ=/nologo /MD /W3 /GX /YX $(OPTIMIZE) /D "_REL_DEB" $(UNICODE_FLAGS) /D "WIN32" /D "NDEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3 /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ \
/c $(AFXDLL) $(DEBUGINFO) $(PDBINFO) $(CFLAGS)
!ELSEIF  "$(CFGREDEFINED)" == "Win32 Unicode Debug"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinDebU
!ENDIF
OUTNAME=$(BASENAME)u$(D_SUFF)
!IF EXIST("unicode.no")
CPP_PROJ=/nologo /W3 /GX $(MLD) $(DEBUGINFO) $(PDBINFO) /YX /Od /D "WIN32" /D "_DEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3 /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c $(CFLAGS)
!ELSE
CPP_PROJ=/nologo /W3 /GX $(MLD) $(DEBUGINFO) $(PDBINFO) /YX /Od $(UNICODE_FLAGS) /D "WIN32" /D "_DEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3 /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c $(CFLAGS)
!ENDIF
!ELSEIF  "$(CFGREDEFINED)" == "Win32 MT Unicode Debug"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinDebMTU
!ENDIF
OUTNAME=$(BASENAME)mu$(D_SUFF)
!IF EXIST("unicode.no")
CPP_PROJ=/nologo /$(MTD) /W3 /GX $(DEBUGINFO) $(PDBINFO) /YX /Od /D "WIN32" /D "_DEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3 /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c $(CFLAGS)
!ELSE
CPP_PROJ=/nologo /$(MTD) /W3 /GX $(DEBUGINFO) $(PDBINFO) /YX /Od $(UNICODE_FLAGS) /D "WIN32" /D "_DEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3 /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c $(CFLAGS)
!ENDIF
!ELSEIF  "$(CFGREDEFINED)" == "Win32 MTD Unicode Debug"
!IF "$(INTDIR)" == "" || "$(INTDIR)" == "."
INTDIR=.\WinDebMDU
!ENDIF
OUTNAME=$(BASENAME)lu$(D_SUFF)
!IF EXIST("unicode.no")
CPP_PROJ=/nologo /$(MDD) /W3 /GX $(DEBUGINFO) $(PDBINFO) /YX /Od /D "WIN32" /D "_DEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3 /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ \
/c $(AFXDLL) $(CFLAGS)
!ELSE
CPP_PROJ=/nologo /$(MDD) /W3 /GX $(DEBUGINFO) $(PDBINFO) /YX /Od $(UNICODE_FLAGS) /D "WIN32" /D "_DEBUG" /D "_WINDOWS" /I $(PROJPATH)\UNITS \
/I $(EIFFEL_S)\RUN3 /Fp$(OUTDIR)/$(OUTNAME).PCH /Fo$(INTDIR)/ /c $(CFLAGS)
!ENDIF
!ENDIF

# ====================================================================================== CS
!ELSEIF "$(TARGETTYPE)" == "CSLIB"
CSTARGET=library
!IF "$(OUTDIR)" == ""
OUTDIR=$(PROJPATH)\$(DIR_PREFIX)\DLL$(DIR_SUFFIX)
!ENDIF
!IF "$(OUTEXT)" == ""
OUTEXT=DLL
!ENDIF
!IF "$(CFG_CS)" == "Win32 Release"
!IF "$(INTDIR)" == ""
INTDIR=.\WinRel
!ENDIF
OUTNAME=$(BASENAME)
LIBSUFFIX=
LIBCFG=Win32 Release
!ELSEIF "$(CFG_CS)" == "Win32 Debug"
!IF "$(INTDIR)" == ""
INTDIR=.\WinDeb
!ENDIF
OUTNAME=$(BASENAME)$(D_SUFF)
LIBSUFFIX=$(D_SUFF)
LIBCFG=Win32 Debug
CS_FLAGS = $(CS_FLAGS) /debug /d:DEBUG
!ELSEIF "$(CFG_CS)" == "Win32 Release Debug"
!IF "$(INTDIR)" == ""
INTDIR=.\WinRelDeb
!ENDIF
OUTNAME=$(BASENAME)$(R_SUFF)
LIBSUFFIX=$(R_SUFF)
LIBCFG=Win32 Release Debug
CS_FLAGS = $(CS_FLAGS) /debug:full /d:TRACE
!ELSEIF "$(CFG_CS)" == "Clean"
!ELSEIF "$(CFG_CS)" == "Clean All"
!ELSE
!ERROR "CFG should be defined as either 'Win32 Release' or 'Win32 Debug'"
!ENDIF

!ELSEIF "$(TARGETTYPE)" == "CSMODULE"
CSTARGET=module
!IF "$(OUTDIR)" == ""
OUTDIR=$(PROJPATH)\$(DIR_PREFIX)\DLL$(DIR_SUFFIX)
!ENDIF
!IF "$(OUTEXT)" == ""
OUTEXT=DLL
!ENDIF
!IF "$(CFG_CS)" == "Win32 Release"
!IF "$(INTDIR)" == ""
INTDIR=.\WinRel
!ENDIF
OUTNAME=$(BASENAME)
LIBSUFFIX=
LIBCFG=Win32 Release
!ELSEIF "$(CFG_CS)" == "Win32 Debug"
!IF "$(INTDIR)" == ""
INTDIR=.\WinDeb
!ENDIF
OUTNAME=$(BASENAME)$(D_SUFF)
LIBSUFFIX=$(D_SUFF)
LIBCFG=Win32 Debug
CS_FLAGS = $(CS_FLAGS) /debug /d:DEBUG
!ELSEIF "$(CFG_CS)" == "Win32 Release Debug"
!IF "$(INTDIR)" == ""
INTDIR=.\WinRelDeb
!ENDIF
OUTNAME=$(BASENAME)$(R_SUFF)
LIBSUFFIX=$(R_SUFF)
LIBCFG=Win32 Release Debug
CS_FLAGS = $(CS_FLAGS) /debug:full /d:TRACE
!ELSEIF "$(CFG_CS)" == "Clean"
!ELSEIF "$(CFG_CS)" == "Clean All"
!ELSE
!ERROR "CFG should be defined as either 'Win32 Release' or 'Win32 Debug'"
!ENDIF

!ELSEIF "$(TARGETTYPE)" == "CSEXE"
#!ERROR CFG=$(CFG_CS); CFGREDEFINED=$(CFGREDEFINED); CFG_CS=$(CFG_CS);
CSTARGET=exe
!IF "$(OUTDIR)" == ""
OUTDIR=$(PROJPATH)\$(DIR_PREFIX)\BIN$(DIR_SUFFIX)
!ENDIF
!IF "$(OUTEXT)" == ""
OUTEXT=EXE
!ENDIF
!IF "$(CFG_CS)" == "Win32 Release"
!IF "$(INTDIR)" == ""
INTDIR=.\WinRel
!ENDIF
OUTNAME=$(BASENAME)
LIBSUFFIX=
LIBCFG=Win32 Release
!ELSEIF "$(CFG_CS)" == "Win32 Debug"
!IF "$(INTDIR)" == ""
INTDIR=.\WinDeb
!ENDIF
OUTNAME=$(BASENAME)$(D_SUFF)
LIBSUFFIX=$(D_SUFF)
LIBCFG=Win32 Debug
CS_FLAGS = $(CS_FLAGS) /debug /d:DEBUG
!ELSEIF "$(CFG_CS)" == "Win32 Release Debug"
!IF "$(INTDIR)" == ""
INTDIR=.\WinRelDeb
!ENDIF
OUTNAME=$(BASENAME)$(R_SUFF)
LIBSUFFIX=$(R_SUFF)
LIBCFG=Win32 Release Debug
CS_FLAGS = $(CS_FLAGS) /debug:full /d:TRACE
!ELSEIF "$(CFG_CS)" == "Clean"
!ELSEIF "$(CFG_CS)" == "Clean All"
!ELSE
!ERROR "CFG should be defined as either 'Win32 Release' or 'Win32 Debug'"
!ENDIF

!ELSEIF "$(TARGETTYPE)" == "CSWINEXE"
CSTARGET=winexe
!IF "$(OUTDIR)" == ""
OUTDIR=$(PROJPATH)\$(DIR_PREFIX)\BIN$(DIR_SUFFIX)
!ENDIF
!IF "$(OUTEXT)" == ""
OUTEXT=EXE
!ENDIF
!IF "$(CFG_CS)" == "Win32 Release"
!IF "$(INTDIR)" == ""
INTDIR=.\WinRel
!ENDIF
OUTNAME=$(BASENAME)
LIBSUFFIX=
LIBCFG=Win32 Release
!ELSEIF "$(CFG_CS)" == "Win32 Debug"
!IF "$(INTDIR)" == ""
INTDIR=.\WinDeb
!ENDIF
OUTNAME=$(BASENAME)$(D_SUFF)
LIBSUFFIX=$(D_SUFF)
LIBCFG=Win32 Debug
CS_FLAGS = $(CS_FLAGS) /debug /d:DEBUG
!ELSEIF "$(CFG_CS)" == "Win32 Release Debug"
!IF "$(INTDIR)" == ""
INTDIR=.\WinRelDeb
!ENDIF
OUTNAME=$(BASENAME)$(R_SUFF)
LIBSUFFIX=$(R_SUFF)
LIBCFG=Win32 Release Debug
CS_FLAGS = $(CS_FLAGS) /debug:full /d:TRACE
!ELSEIF "$(CFG_CS)" == "Clean"
!ELSEIF "$(CFG_CS)" == "Clean All"
!ELSE
!ERROR "CFG should be defined as either 'Win32 Release' or 'Win32 Debug'"
!ENDIF

# ====================================================================================== CS

!ENDIF

################################################################################
# Begin Project
!IFNDEF CPP
CPP=cl.exe
!ENDIF
CPP_OBJS=$(INTDIR)

LIB32=lib.exe
LIB32_FLAGS=/NOLOGO /OUT:$(OUTDIR)\$(OUTNAME).$(OUTEXT) $(LIBFLAGS)

BSC32=bscmake.exe
BSC32_FLAGS=/nologo /o$(OUTDIR)\$(OUTNAME).bsc $(BFLAGS)

!IFNDEF LINK32
LINK32=link.exe
!ENDIF

LINK32_FLAGS= /NOLOGO /SUBSYSTEM:$(SUBSYSTEM) /INCREMENTAL:no \
 /MAP:$(INTDIR)/$(OUTNAME).map /MACHINE:I386\
 /OUT:$(OUTDIR)/$(OUTNAME).$(OUTEXT) $(LFLAGS)

#!IFNDEF VC7
#LINK32_FLAGS=/PDB:NONE $(LINK32_FLAGS)
#!ENDIF

!IF "$(TARGETTYPE)" != "OCX"
LINK32_LIBS=$(DEFAULTLIBS) kernel32.lib user32.lib gdi32.lib winspool.lib comdlg32.lib\
 advapi32.lib shell32.lib ole32.lib oleaut32.lib uuid.lib odbc32.lib\
 odbccp32.lib
!ENDIF

RSC=rc.exe /i $(PROJPATH)

DEF_FLAGS=
DEF_FILE=

#!IFDEF VC7
#
#!IFDEF NET20 # ========================== C#
#!ERROR Not Rready yet
#!ELSEIFDEF NET11
#CS=$(SYSTEMROOT)\Microsoft.NET\Framework\v1.1.4322\csc.exe
#!ELSEIFDEF NET10
#CS=$(SYSTEMROOT)\Microsoft.NET\Framework\v1.0.3705\csc.exe
#!ELSE
#!ERROR "NET Version is not defined"
#!ENDIF
#
#!ENDIF

MAKELIBCMD=if not exist makefile. copy $(PROJPATH)\makefile.t makefile.;\
 $(MAKE) /$(MAKEFLAGS) DEPEND; $(MAKE) /$(MAKEFLAGS) CFG="$(LIBCFG)"

################################################################################
#   Your filedefs.inc file will be included at this point.                     #
#  It should define symbols: LIB32_OBJS, SRC32_CPPS, LIB32_LIBS                #
#                                                                              #
#  It might not define symbol BSC32_SBRS since browser symbol cache            #
#  information is no longer supported.                                         #
################################################################################

#!IF EXIST("srcdefs.inc")
#!INCLUDE "srcdefs.inc"
#!IF EXIST("objdefs.inc")
#!INCLUDE "objdefs.inc"
#!ELSE
#!ERROR Can not find 'objdefs.inc' file
#!ENDIF
#!ELSE IF EXIST("filedefs.inc")
#!INCLUDE "filedefs.inc"
#!ELSE
#!ERROR "No files with source definitions found"
#!ENDIF

!IF EXIST("srcdefs.inc")
!ELSE IF EXIST("filedefs.inc")
!INCLUDE "filedefs.inc"
!ELSE
!ERROR "No files with source definitions found"
!ENDIF

!MESSAGE
!MESSAGE ----------- UNIT = "$(MAKEDIR)"; $(CURDATE) $(CURTIME); CFG = "$(CFGREDEFINED)" -----------

################################################################################

#!IF "$(TARGETTYPE)" != "LIB" # !!! EUG - Found out why it was prohibited to look for
                              # dependencies for LIBS

!IFNDEF NODEPENDENTBUILD

#!IF "$(TARGETTYPE)" == "LIB"
#LIBCFG=$(CFGREDEFINED)
#!ENDIF

TARGET : MAKELIBS TARGET1

!IF "$(CFG)" == "Clean" \
       || "$(CFG)" == "Clean All"
LIBCFG=$(CFG)
!ENDIF

MAKELIBS : $(LIB32_LIBS)
#!ERROR $(MAKELIB) $(**) $(MAKE) "$(LIBCFG)" $(MAKEDIR) $(MAKEFLAGS)
!IF "$(LIB32_LIBS)" != ""
    !call $(MAKELIB) $(**) $(MAKE) "$(LIBCFG)" $(MAKEDIR) $(MAKEFLAGS)
    @echo.
    @echo ----------- UNIT = "$(MAKEDIR)"; $(CURDATE) $(CURTIME); CFG = "$(LIBCFG)" -----------
!ELSE
    @echo. > NUL
!ENDIF

#    !call $(PROJPATH)\makelib $(**) $(MAKE) "$(LIBCFG)" $(MAKEDIR) /$(MAKEFLAGS)

!ENDIF

#!ENDIF # !IF "$(TARGETTYPE)" != "LIB"

!IF ( "$(TARGETTYPE)" != "TYPLIB" ) && ( "$(TARGETTYPE)" != "DEPENDENCIES" )

TARGET1 : $(OUTDIR)/$(OUTNAME).$(OUTEXT)        # $(OUTDIR)/$(OUTNAME1).bsc
    @if exist $(OUTDIR)\$(OUTNAME).PCH del $(OUTDIR)\$(OUTNAME).PCH
    @if exist $(PROJPATH)\IMPLIB\$(OUTNAME).EXP del $(PROJPATH)\IMPLIB\$(OUTNAME).EXP

!IF "$(TARGETTYPE)" == "LIB"
$(OUTDIR)/$(OUTNAME).$(OUTEXT) : $(OUTDIR)  $(DEF_FILE) $(LINK32_OBJS)
    @echo.
    @echo Linking $(OUTDIR)/$(OUTNAME).$(OUTEXT)...
    @echo $(LIB32) $(LINK32_OBJS) $(LIB32_FLAGS)
    $(LIB32) @<<
  $(LINK32_OBJS) $(LIB32_FLAGS)
<<
!ELSEIF "$(TARGETTYPE)" == "CSLIB"    \
     || "$(TARGETTYPE)" == "CSEXE"    \
     || "$(TARGETTYPE)" == "CSWINEXE" \
     || "$(TARGETTYPE)" == "CSMODULE"

#!IF "$(DIR_PREFIX)" == ""
#!IF "$(CFG)" == "Win32 Debug"
#DIR_PREFIX=Debug
#!ELSE
#DIR_PREFIX=Release
#!ENDIF
#!ENDIF

CS_FLAGS=$(CS_FLAGS) /d:TRACE

#RES32_DUMMY_TARGET ::
#!IFDEF RES32_RES
#    @$(PROJDIR)\perl $(PROJDIR)\resx.pl <<c:\temp\resx.tmp
#$(MAKE)
#$(RES32_RES)
#<<KEEP
#!ENDIF

#LICENSES_DUMMY_TARGET ::
#!IFDEF LICENSES
#    @$(PROJDIR)\perl $(PROJDIR)\licx.pl <<
#$(MAKE)
#$(OUTDIR)
#$(OUTNAME).$(OUTEXT)
#$(INTDIR)
#$(LICENSES)
#<<
#RES32_LICENSES=/res:$(INTDIR)\$(OUTNAME).$(OUTEXT).licenses
#!ENDIF

TEMP_VAR=$(CS) $(CS_FLAGS) /out:$(OUTDIR)\$(OUTNAME).$(OUTEXT) /target:$(CSTARGET) $(REF32_CS) $(SRC32_CS) $(RES32_LICENSES)

#TEST_DUMMY_TARGET :
#    @$(PROJDIR)\perl $(PROJDIR)\make4net.pl <<c:\temp\1.tmp <<c:\temp\2.tmp
#first file qq
#<<KEEP
#second file ww
#<<KEEP

CS_DUMMY_TARGET :
    @$(PROJPATH)\perl $(PROJPATH)\make4net.pl <<$(xINPUT4PERL) <<$(xINPUT4MAKE) <<$(xINPUT4CS)
$(MAKE)
$(MAKEFLAGS)
$(MAKEDIR)
$(CSTARGET)
$(CS)
$(CS_FLAGS)
$(INTDIR)
$(OUTDIR)
$(OUTNAME)
$(OUTEXT)
$(SRC32_CS)
$(RES32_RES)
$(REF32_CS)
$(LICENSES)
$(RCW)
<<$(KEEP_TEMP_FILES)
<<$(KEEP_TEMP_FILES)
<<$(KEEP_TEMP_FILES)

$(OUTDIR)\$(OUTNAME).$(OUTEXT) : \
                                 $(INTDIR) \
                                 $(OUTDIR) \
                                 CS_DUMMY_TARGET
#                                 $(REF32_CS:/r:=) \
#!IFDEF RES32_RES
#                                 RES32_DUMMY_TARGET \
#!ENDIF
#!IFDEF LICENSES
#                                 LICENSES_DUMMY_TARGET \
#!ENDIF
#                                 $(SRC32_CS:/res:=)
#    $(CS) @<<
#$(CS_FLAGS) /out:$(OUTDIR)\$(OUTNAME).$(OUTEXT) /target:$(CSTARGET) $(REF32_CS) $(SRC32_CS) $(RES32_LICENSES)
#<<

#!IFDEF CS_LOGFILE
#  echo $(TEMP_VAR) >> $(CS_LOGFILE)
#!ENDIF

!ELSE
$(OUTDIR)/$(OUTNAME).$(OUTEXT) : $(OUTDIR)  $(DEF_FILE) $(LINK32_OBJS)
    @echo.
    @echo Linking $(OUTDIR)/$(OUTNAME).$(OUTEXT)...
    @echo $(LINK32) $(LINK32_OBJS) $(LINK32_LIBS) $(LINK32_FLAGS)
    $(LINK32) @<<
  $(LINK32_OBJS) $(LINK32_LIBS) $(LINK32_FLAGS)
<<
!ENDIF # "$(TARGETTYPE)" == "LIB"

!ELSE
TARGET1:
#    @echo TYPLIB

!ENDIF # "$(TARGETTYPE)" != "TYPLIB" && ( "$(TARGETTYPE)" != "DEPENDENCIES" )

#
#  Inference rules for BSC
#
#$(OUTDIR)/$(OUTNAME).bsc : $(OUTDIR)  $(BSC32_SBRS)
#    $(BSC32) @<<
#  $(BSC32_FLAGS) $(BSC32_SBRS)
#<<

!IF "$(TARGETTYPE)" != "TYPLIB" \
    && "$(TARGETTYPE)" != "DEPENDENCIES"

!IF "$(CFG)" != "Clean" \
    && "$(CFG)" != "Clean All"
$(OUTDIR) :
    if not exist $(OUTDIR)/nul mkdir $(OUTDIR)

!IFDEF INTDIR
$(INTDIR) :
    if not exist $(INTDIR)/nul mkdir $(INTDIR)
!ENDIF

!ENDIF

!ENDIF

################################################################################
#   Your usrrules.inc file will be included at this point.                     #
################################################################################

## ##!IF EXIST("srcdefs.inc")
## !IF EXIST("atfirst.inc")
## !INCLUDE "atfirst.inc"
## ##!ENDIF
## !ELSE IF EXIST("usrrules.inc")
## !IF EXIST("atfirst.inc")
## !ERROR "usrrules.inc" should not exist with "atfirst.inc" in one directory
## !ENDIF
## !INCLUDE "usrrules.inc"
## !ELSE
## USRTARGET:
## !ENDIF

USRTARGET:

!IF EXIST("usrrules.inc")
!INCLUDE "usrrules.inc"
!ENDIF
!IF EXIST("atfirst.inc")
!INCLUDE "atfirst.inc"
!ENDIF

ALL :
!IF "$(LIGHT)" == "YES"
    -sh -c "echo ERR > $(PROJPATH)/log/$(BASENAME).err"
!ENDIF
    $(MAKE) /$(MAKEFLAGS) DEPEND
!IF "$(TARGETTYPE)" != "LIB" || $(ST_FLAVOUR) == 1
    $(MAKE) /$(MAKEFLAGS) CFG="Win32 Debug"
    $(MAKE) /$(MAKEFLAGS) CFG="Win32 Release"
!IF "$(UNIBUILD)" == "TRUE"
    $(MAKE) /$(MAKEFLAGS) CFG="Win32 Unicode Debug"
    $(MAKE) /$(MAKEFLAGS) CFG="Win32 Unicode Release"
!ENDIF
!IF "$(LIGHT)" == "YES"
    -sh -c "mv $(PROJPATH)/log/$(BASENAME).err $(PROJPATH)/log/$(BASENAME).ok"
!ENDIF
!ENDIF
!IF "$(TARGETTYPE)" == "LIB"
!IF "$(LIGHT)" == "YES"
    -sh -c "echo ERR > $(PROJPATH)/log/$(BASENAME).err"
!ENDIF
!IF $(MT_FLAVOUR) == 1
    $(MAKE) /$(MAKEFLAGS) CFG="Win32 MT Debug"
    $(MAKE) /$(MAKEFLAGS) CFG="Win32 MT Release"
!IF "$(UNIBUILD)" == "TRUE"
    $(MAKE) /$(MAKEFLAGS) CFG="Win32 MT Unicode Debug"
    $(MAKE) /$(MAKEFLAGS) CFG="Win32 MT Unicode Release"
!ENDIF
!ENDIF
!IF $(MD_FLAVOUR) == 1
    $(MAKE) /$(MAKEFLAGS) CFG="Win32 MTD Debug"
    $(MAKE) /$(MAKEFLAGS) CFG="Win32 MTD Release"
!IF "$(UNIBUILD)" == "TRUE"
    $(MAKE) /$(MAKEFLAGS) CFG="Win32 MTD Unicode Debug"
    $(MAKE) /$(MAKEFLAGS) CFG="Win32 MTD Unicode Release"
!ENDIF
!ENDIF
!IF "$(LIGHT)" == "YES"
    -sh -c "mv $(PROJPATH)/log/$(BASENAME).err $(PROJPATH)/log/$(BASENAME).ok"
!ENDIF
!ENDIF

.c{$(CPP_OBJS)}.obj:
   @perl -e 'print "Something wrong while creating USRTARGET and (or) DEPENDENCIES.\n"; exit 1;'
   $(CPP) /D "EUG_ERROR" $(CPP_PROJ) $<

.cpp{$(CPP_OBJS)}.obj:
   @perl -e 'print "Something wrong while creating USRTARGET and (or) DEPENDENCIES.\n"; exit 1;'
   $(CPP) /D "EUG_ERROR" $(CPP_PROJ) $<

.cxx{$(CPP_OBJS)}.obj:
   @perl -e 'print "Something wrong while creating USRTARGET and (or) DEPENDENCIES.\n"; exit 1;'
   $(CPP) /D "EUG_ERROR" $(CPP_PROJ) $<

.idl.tlb:
   @perl -e 'print "Something wrong while creating USRTARGET and (or) DEPENDENCIES.\n"; exit 1;'
   @echo "qq" $<

################################################################################

#DEPEND: USRTARGET
#    makedep @<<
#  -I$(PROJPATH)/units -I "$(INCLUDE)" -i $(SRC32_CPPS)
#<<

DEPEND: USRTARGET
    @if exist $(OUTDIR)\$(OUTNAME).PCH del $(OUTDIR)\$(OUTNAME).PCH > NUL
    @echo.
    @echo Building Dependencies ...
!IF "$(MAKEDEP_OUT)" == ""
    @makedep @<<
  -I$(PROJPATH)/units -I "$(INCLUDE)" -i $(SRC32_CPPS)
<<
!ELSE
    @makedep >> $(MAKEDEP_OUT) 2>>&1 @<<
  -I$(PROJPATH)/units -I "$(INCLUDE)" -i $(SRC32_CPPS)
<<
!ENDIF

CLEANALL:: MAKELIBS CLEAN

CLEAN::
!IF "$(TARGETTYPE)" != "TYPLIB"
    @rm -f $(OUTDIR)\$(BASENAME)*.$(OUTEXT)
    @if exist $(OUTDIR)\$(BASENAME)*.PDB rm -f $(OUTDIR)\$(BASENAME)*.PDB
    @if exist $(PROJPATH)\PDB\$(BASENAME)*.PDB rm -f $(PROJPATH)\PDB\$(BASENAME)*.PDB
#    echo @rm -f $(PDBINFO:/Fd=)
    @rm -f $(OUTDIR)\$(BASENAME)*.PCH
    @-rmdir /q $(OUTDIR) > NUL 2>>&1
    @-rmdir /q $(PROJPATH)\PDB > NUL 2>>&1
!ENDIF # "$(TARGETTYPE)" != "TYPLIB"
!IF "$(TARGETTYPE)" == "DLL"                 \
       || "$(TARGETTYPE)" != "ATLDLL"        \
       || "$(TARGETTYPE)" != "DLL_STATIC"    \
       || "$(TARGETTYPE)" != "OCX"           \
       || "$(TARGETTYPE)" != "DLL_SCAN"      \
       || "$(TARGETTYPE)" != "MIXED_DLL"
    @rm -f $(PROJPATH)\IMPLIB\$(BASENAME)*.LIB
    @-rmdir /q $(PROJPATH)\IMPLIB > NUL 2>>&1
!ENDIF

!IF EXIST("clndefs.inc")
!MESSAGE clndefs included
!INCLUDE "clndefs.inc"
!ENDIF
# DO NOT DELETE
