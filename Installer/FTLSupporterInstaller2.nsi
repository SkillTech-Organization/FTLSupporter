; Script generated by the HM NIS Edit Script Wizard.

; HM NIS Edit Wizard helper defines
!define PRODUCT_NAME "FTLSupporter"
!define PRODUCT_VERSION "3.7.1"
!define PRODUCT_PUBLISHER "Pratix Kft."
!define PRODUCT_UNINST_KEY "Software\Microsoft\Windows\CurrentVersion\Uninstall\${PRODUCT_NAME}"
!define PRODUCT_UNINST_ROOT_KEY "HKLM"

; MUI 1.67 compatible ------
!include "MUI.nsh"

; MUI Settings
!define MUI_ABORTWARNING
!define MUI_ICON "${NSISDIR}\Contrib\Graphics\Icons\modern-install.ico"
!define MUI_UNICON "${NSISDIR}\Contrib\Graphics\Icons\modern-uninstall.ico"

; Welcome page
!insertmacro MUI_PAGE_WELCOME
; Directory page
!insertmacro MUI_PAGE_DIRECTORY
; Instfiles page
!insertmacro MUI_PAGE_INSTFILES
; Finish page
!insertmacro MUI_PAGE_FINISH

; Uninstaller pages
!insertmacro MUI_UNPAGE_INSTFILES

; Language files
!insertmacro MUI_LANGUAGE "Hungarian"

; MUI end ------

Name "${PRODUCT_NAME} ${PRODUCT_VERSION}"
OutFile "FTLSupporterSetup.exe"
InstallDir "$PROGRAMFILES\FTLSupporter"
ShowInstDetails show
ShowUnInstDetails show

Function .onInstSuccess

FunctionEnd

Section "F�szakasz" SEC01
  SetOutPath "$INSTDIR"
  SetOverwrite on
  File "..\FTLSupporter\bin\Release\*.dll"
 
SectionEnd

Section -AdditionalIcons
  CreateDirectory "$SMPROGRAMS\FTLSupporter"
  CreateShortCut "$SMPROGRAMS\FTLSupporter\Uninstall.lnk" "$INSTDIR\uninst.exe"
SectionEnd

Section -Post
  WriteUninstaller "$INSTDIR\uninst.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayName" "$(^Name)"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "UninstallString" "$INSTDIR\uninst.exe"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "DisplayVersion" "${PRODUCT_VERSION}"
  WriteRegStr ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}" "Publisher" "${PRODUCT_PUBLISHER}"
SectionEnd


Function un.onUninstSuccess
  HideWindow
  MessageBox MB_ICONINFORMATION|MB_OK "A(z) $(^Name) sikeresen el lett t�vol�tva a sz�m�t�g�p�r�l."
FunctionEnd

Function un.onInit
  MessageBox MB_ICONQUESTION|MB_YESNO|MB_DEFBUTTON2 "Biztosan teljesen el szeretn� t�vol�tani a(z) $(^Name)-t �s az annak �sszes �sszetev�j�t?" IDYES +2
  Abort
FunctionEnd

Section Uninstall
 
  Delete "$INSTDIR\uninst.exe"
  Delete "$INSTDIR\regasm.exe"
  Delete "$INSTDIR\*.dll"
  Delete "$INSTDIR\*.tlb"

  Delete "$SMPROGRAMS\PMap\Uninstall.lnk"

  RMDir "$SMPROGRAMS\FTLSupporter"
  RMDir "$INSTDIR"

  DeleteRegKey ${PRODUCT_UNINST_ROOT_KEY} "${PRODUCT_UNINST_KEY}"
  SetAutoClose true
SectionEnd