///////////////////////////////////////////////////////////////////////////////
// Copyright � 2002, Open Design Alliance Inc. ("Open Design") 
// 
// This software is owned by Open Design, and may only be incorporated into 
// application programs owned by members of Open Design subject to a signed 
// Membership Agreement and Supplemental Software License Agreement with 
// Open Design. The structure and organization of this Software are the valuable 
// trade secrets of Open Design and its suppliers. The Software is also protected 
// by copyright law and international treaty provisions. You agree not to 
// modify, adapt, translate, reverse engineer, decompile, disassemble or 
// otherwise attempt to discover the source code of the Software. Application 
// programs incorporating this software must include the following statement 
// with their copyright notices:
//
//      DWGdirect � 2002 by Open Design Alliance Inc. All rights reserved. 
//
// By use of this software, you acknowledge and accept the terms of this 
// agreement.
///////////////////////////////////////////////////////////////////////////////

#ifndef _INC_DDBRBREPSHELLTRAVERSER_3F83F556035B_INCLUDED
#define _INC_DDBRBREPSHELLTRAVERSER_3F83F556035B_INCLUDED
#include "BrBrep.h"
#include "BrShell.h"


#include "BrTraverser.h"

#include "DD_PackPush.h"


/** Description:
    Interface class for B-Rep shell traversers.

    {group:OdBr_Classes}
*/
class ODBR_TOOLKIT_EXPORT OdBrBrepShellTraverser : public OdBrTraverser
{
public:
  OdBrErrorStatus setBrep(const OdBrBrep& OdBrep);
  void setBrepAndShell(const OdBrShell& shell);
  void setShell(const OdBrShell& shell);

  OdBrShell getShell() const;
  OdBrBrep getBrep() const;

  OdBrBrepShellTraverser();
};

#include "DD_PackPop.h"

#endif /* _INC_DDBRBREPSHELLTRAVERSER_3F83F556035B_INCLUDED */


