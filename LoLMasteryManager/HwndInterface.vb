' Adapted from WindowScrape (https://github.com/DataDink/WindowScrape)
' By Mark Greenwald

Imports System.Runtime.InteropServices
Imports System.Text

Public Enum WM As UInteger
    ACTIVATE = 6
    ACTIVATEAPP = &H1C
    AFXFIRST = &H360
    AFXLAST = &H37F
    APP = &H8000
    APPCOMMAND = &H319
    ASKCBFORMATNAME = 780
    BN_CLICKED = &HF5
    CANCELJOURNAL = &H4B
    CANCELMODE = &H1F
    CAPTURECHANGED = &H215
    CHANGECBCHAIN = &H30D
    CHANGEUISTATE = &H127
    [CHAR] = &H102
    CHARTOITEM = &H2F
    CHILDACTIVATE = &H22
    CLEAR = &H303
    CLIPBOARDUPDATE = &H31D
    CLOSE = &H10
    COMMAND = &H111
    <Obsolete>
    COMMNOTIFY = &H44
    COMPACTING = &H41
    COMPAREITEM = &H39
    CONTEXTMENU = &H7B
    COPY = &H301
    COPYDATA = &H4A
    CPL_LAUNCH = &H1400
    CPL_LAUNCHED = &H1401
    CREATE = 1
    CTLCOLORBTN = &H135
    CTLCOLORDLG = 310
    CTLCOLOREDIT = &H133
    CTLCOLORLISTBOX = &H134
    CTLCOLORMSGBOX = &H132
    CTLCOLORSCROLLBAR = &H137
    CTLCOLORSTATIC = &H138
    CUT = &H300
    DEADCHAR = &H103
    DELETEITEM = &H2D
    DESTROY = 2
    DESTROYCLIPBOARD = &H307
    DEVICECHANGE = &H219
    DEVMODECHANGE = &H1B
    DISPLAYCHANGE = &H7E
    DRAWCLIPBOARD = &H308
    DRAWITEM = &H2B
    DROPFILES = &H233
    DWMCOLORIZATIONCOLORCHANGED = 800
    DWMCOMPOSITIONCHANGED = &H31E
    DWMNCRENDERINGCHANGED = &H31F
    DWMWINDOWMAXIMIZEDCHANGE = &H321
    ENABLE = 10
    ENDSESSION = &H16
    ENTERIDLE = &H121
    ENTERMENULOOP = &H211
    ENTERSIZEMOVE = &H231
    ERASEBKGND = 20
    EXITMENULOOP = 530
    EXITSIZEMOVE = &H232
    FONTCHANGE = &H1D
    GETDLGCODE = &H87
    GETFONT = &H31
    GETHOTKEY = &H33
    GETICON = &H7F
    GETMINMAXINFO = &H24
    GETOBJECT = &H3D
    GETTEXT = 13
    GETTEXTLENGTH = 14
    GETTITLEBARINFOEX = &H33F
    HANDHELDFIRST = &H358
    HANDHELDLAST = &H35F
    HELP = &H53
    HOTKEY = &H312
    HSCROLL = &H114
    HSCROLLCLIPBOARD = &H30E
    ICONERASEBKGND = &H27
    IME_CHAR = &H286
    IME_COMPOSITION = &H10F
    IME_COMPOSITIONFULL = &H284
    IME_CONTROL = &H283
    IME_ENDCOMPOSITION = 270
    IME_KEYDOWN = &H290
    IME_KEYLAST = &H10F
    IME_KEYUP = &H291
    IME_NOTIFY = &H282
    IME_REQUEST = &H288
    IME_SELECT = &H285
    IME_SETCONTEXT = &H281
    IME_STARTCOMPOSITION = &H10D
    INITDIALOG = &H110
    INITMENU = &H116
    INITMENUPOPUP = &H117
    INPUT = &HFF
    INPUT_DEVICE_CHANGE = &HFE
    INPUTLANGCHANGE = &H51
    INPUTLANGCHANGEREQUEST = 80
    KEYDOWN = &H100
    KEYFIRST = &H100
    KEYLAST = &H109
    KEYUP = &H101
    KILLFOCUS = 8
    LBUTTONDBLCLK = &H203
    LBUTTONDOWN = &H201
    LBUTTONUP = &H202
    MBUTTONDBLCLK = &H209
    MBUTTONDOWN = &H207
    MBUTTONUP = 520
    MDIACTIVATE = &H222
    MDICASCADE = &H227
    MDICREATE = &H220
    MDIDESTROY = &H221
    MDIGETACTIVE = &H229
    MDIICONARRANGE = &H228
    MDIMAXIMIZE = &H225
    MDINEXT = &H224
    MDIREFRESHMENU = &H234
    MDIRESTORE = &H223
    MDISETMENU = 560
    MDITILE = 550
    MEASUREITEM = &H2C
    MENUCHAR = &H120
    MENUCOMMAND = &H126
    MENUDRAG = &H123
    MENUGETOBJECT = &H124
    MENURBUTTONUP = 290
    MENUSELECT = &H11F
    MOUSEACTIVATE = &H21
    MOUSEFIRST = &H200
    MOUSEHOVER = &H2A1
    MOUSEHWHEEL = &H20E
    MOUSELAST = &H20E
    MOUSELEAVE = &H2A3
    MOUSEMOVE = &H200
    MOUSEWHEEL = &H20A
    MOVE = 3
    MOVING = &H216
    NCACTIVATE = &H86
    NCCALCSIZE = &H83
    NCCREATE = &H81
    NCDESTROY = 130
    NCHITTEST = &H84
    NCLBUTTONDBLCLK = &HA3
    NCLBUTTONDOWN = &HA1
    NCLBUTTONUP = &HA2
    NCMBUTTONDBLCLK = &HA9
    NCMBUTTONDOWN = &HA7
    NCMBUTTONUP = &HA8
    NCMOUSEHOVER = &H2A0
    NCMOUSELEAVE = &H2A2
    NCMOUSEMOVE = 160
    NCPAINT = &H85
    NCRBUTTONDBLCLK = &HA6
    NCRBUTTONDOWN = &HA4
    NCRBUTTONUP = &HA5
    NCXBUTTONDBLCLK = &HAD
    NCXBUTTONDOWN = &HAB
    NCXBUTTONUP = &HAC
    NEXTDLGCTL = 40
    NEXTMENU = &H213
    NOTIFY = &H4E
    NOTIFYFORMAT = &H55
    NULL = 0
    PAINT = 15
    PAINTCLIPBOARD = &H309
    PAINTICON = &H26
    PALETTECHANGED = &H311
    PALETTEISCHANGING = &H310
    PARENTNOTIFY = &H210
    PASTE = 770
    PENWINFIRST = &H380
    PENWINLAST = &H38F
    <Obsolete>
    POWER = &H48
    POWERBROADCAST = &H218
    PRINT = &H317
    PRINTCLIENT = &H318
    QUERYDRAGICON = &H37
    QUERYENDSESSION = &H11
    QUERYNEWPALETTE = &H30F
    QUERYOPEN = &H13
    QUERYUISTATE = &H129
    QUEUESYNC = &H23
    QUIT = &H12
    RBUTTONDBLCLK = &H206
    RBUTTONDOWN = &H204
    RBUTTONUP = &H205
    RENDERALLFORMATS = &H306
    RENDERFORMAT = &H305
    SETCURSOR = &H20
    SETFOCUS = 7
    SETFONT = &H30
    SETHOTKEY = 50
    SETICON = &H80
    SETREDRAW = 11
    SETTEXT = 12
    SETTINGCHANGE = &H1A
    SHOWWINDOW = &H18
    SIZE = 5
    SIZECLIPBOARD = &H30B
    SIZING = &H214
    SPOOLERSTATUS = &H2A
    STYLECHANGED = &H7D
    STYLECHANGING = &H7C
    SYNCPAINT = &H88
    SYSCHAR = &H106
    SYSCOLORCHANGE = &H15
    SYSCOMMAND = &H112
    SYSDEADCHAR = &H107
    SYSKEYDOWN = 260
    SYSKEYUP = &H105
    SYSTIMER = 280
    TABLET_FIRST = &H2C0
    TABLET_LAST = &H2DF
    TCARD = &H52
    THEMECHANGED = &H31A
    TIMECHANGE = 30
    TIMER = &H113
    UNDO = &H304
    UNICHAR = &H109
    UNINITMENUPOPUP = &H125
    UPDATEUISTATE = &H128
    USER = &H400
    USERCHANGED = &H54
    VKEYTOITEM = &H2E
    VSCROLL = &H115
    VSCROLLCLIPBOARD = &H30A
    WINDOWPOSCHANGED = &H47
    WINDOWPOSCHANGING = 70
    WININICHANGE = &H1A
    WTSSESSION_CHANGE = &H2B1
    XBUTTONDBLCLK = &H20D
    XBUTTONDOWN = &H20B
    XBUTTONUP = &H20C
End Enum

''' <summary>
''' Possible nCmdShow values for ShowWindow.
''' </summary>
''' <remarks>https://msdn.microsoft.com/en-us/library/windows/desktop/ms633548(v=vs.85).aspx</remarks>
Public Enum SW As UInteger

    ''' <summary>
    ''' Hides the window and activates another window.
    ''' </summary>
    HIDE = 0

    ''' <summary>
    ''' Activates and displays a window. If the window is minimized or maximized, the system restores it to its original size and position. An application should specify this flag when displaying the window for the first time.
    ''' </summary>
    SHOWNORMAL = 1

    ''' <summary>
    ''' Activates the window and displays it as a minimized window.
    ''' </summary>
    SHOWMINIMIZED = 2

    ''' <summary>
    ''' Maximizes the specified window.
    ''' </summary>
    MAXIMIZE = 3

    ''' <summary>
    ''' Activates the window and displays it as a maximized window.
    ''' </summary>
    SHOWMAXIMIZED = 3

    ''' <summary>
    ''' Displays a window in its most recent size and position. This value is similar to SW_SHOWNORMAL, except that the window is not activated.
    ''' </summary>
    SHOWNOACTIVATE = 4

    ''' <summary>
    ''' Activates the window and displays it in its current size and position.
    ''' </summary>
    SHOW = 5

    ''' <summary>
    ''' Displays the window as a minimized window. This value is similar to <see cref="SHOWMINIMIZED"/>, except the window is not activated.
    ''' </summary>
    SHOWMINNOACTIVE = 7

    ''' <summary>
    ''' Displays the window in its current size and position. This value is similar to <see cref="SHOW"/>, except that the window is not activated.
    ''' </summary>
    SHOWNA = 8

    ''' <summary>
    ''' Activates and displays the window. If the window is minimized or maximized, the system restores it to its original size and position. An application should specify this flag when restoring a minimized window.
    ''' </summary>
    RESTORE = 9
    ''' <summary>
    ''' Sets the show state based on the SW_ value specified in the STARTUPINFO structure passed to the CreateProcess function by the program that started the application.
    ''' </summary>
    ''' 
    SHOWDEFAULT = 10

    ''' <summary>
    ''' Minimizes a window, even if the thread that owns the window is not responding. This flag should only be used when minimizing windows from a different thread.
    ''' </summary>
    FORCEMINIMIZE = 11

End Enum

<StructLayout(LayoutKind.Sequential)>
Public Structure RECT

    Public Left As Integer
    Public Top As Integer
    Public Right As Integer
    Public Bottom As Integer

End Structure

Friend NotInheritable Class HwndInterface

    Private Sub New()
    End Sub

    Public Shared Function ActivateWindow(hwnd As IntPtr) As Boolean

        Return ShowWindow(hwnd, SW.RESTORE) AndAlso SetForegroundWindow(hwnd)

    End Function

    Public Shared Sub ClickHwnd(hwnd As IntPtr)

        SendMessage(hwnd, &HF5, IntPtr.Zero, IntPtr.Zero)

    End Sub

    <DllImport("user32.dll")>
    Private Shared Function ShowWindow(hWnd As IntPtr, nCmdShow As UInteger) As Boolean
    End Function

    <DllImport("user32.dll")>
    Private Shared Function CloseWindow(hWnd As IntPtr) As Boolean
    End Function

    Public Shared Function EnumChildren(hwnd As IntPtr) As List(Of IntPtr)

        Dim zero As IntPtr = IntPtr.Zero
        Dim list As New List(Of IntPtr)()

        Do
            zero = FindWindowEx(hwnd, zero, Nothing, Nothing)

            If zero <> IntPtr.Zero Then

                list.Add(zero)

            End If

        Loop While zero <> IntPtr.Zero

        Return list

    End Function

    Public Shared Function EnumHwnds() As List(Of IntPtr)
        Return EnumChildren(IntPtr.Zero)
    End Function

    <DllImport("user32.dll")>
    Private Shared Function FindWindow(lpClassName As String, lpWindowName As String) As Integer
    End Function

    <DllImport("user32.dll", SetLastError:=True)>
    Private Shared Function FindWindowEx(parentHandle As IntPtr, childAfter As IntPtr, className As String, windowTitle As String) As IntPtr
    End Function

    <DllImport("user32.dll", CharSet:=CharSet.Auto)>
    Private Shared Function GetClassName(hWnd As IntPtr, lpClassName As StringBuilder, nMaxCount As Integer) As Integer
    End Function

    Public Shared Function GetHwnd(windowText As String, className As String) As IntPtr

        Return New IntPtr(FindWindow(className, windowText))

    End Function

    Public Shared Function GetHwndChild(hwnd As IntPtr, clsName As String, ctrlText As String) As IntPtr

        Return FindWindowEx(hwnd, IntPtr.Zero, clsName, ctrlText)

    End Function

    Public Shared Function GetHwndClassName(hwnd As IntPtr) As String

        Dim lpClassName As New StringBuilder(&H100)

        GetClassName(hwnd, lpClassName, lpClassName.MaxCapacity)

        Return lpClassName.ToString()

    End Function

    Public Shared Function GetHwndFromClass(className As String) As IntPtr

        Return New IntPtr(FindWindow(className, Nothing))

    End Function

    Public Shared Function GetHwndFromTitle(windowText As String) As IntPtr

        Return New IntPtr(FindWindow(Nothing, windowText))

    End Function

    Public Shared Function GetHwndParent(hwnd As IntPtr) As IntPtr

        Return GetParent(hwnd)

    End Function

    Public Shared Function GetHwndPos(hwnd As IntPtr) As Point

        Dim lpRect As New RECT

        GetWindowRect(hwnd, lpRect)

        Return New Point(lpRect.Left, lpRect.Top)

    End Function

    Public Shared Function GetHwndSize(hwnd As IntPtr) As Size

        Dim lpRect As New RECT

        GetWindowRect(hwnd, lpRect)

        Return New Size(lpRect.Right - lpRect.Left, lpRect.Bottom - lpRect.Top)

    End Function

    Public Shared Function GetHwndText(hwnd As IntPtr) As String

        Dim capacity As Integer = CInt(SendMessage(hwnd, 14, 0, 0)) + 1
        Dim lParam As New StringBuilder(capacity)

        SendMessage(hwnd, 13, CUInt(capacity), lParam)

        Return lParam.ToString()

    End Function

    Public Shared Function GetHwndTitle(hwnd As IntPtr) As String

        Dim lpString As New StringBuilder(GetHwndTitleLength(hwnd) + 1)

        GetWindowText(hwnd, lpString, lpString.Capacity)

        Return lpString.ToString()

    End Function

    Public Shared Function GetHwndTitleLength(hwnd As IntPtr) As Integer

        Return GetWindowTextLength(hwnd)

    End Function

    Public Shared Function GetMessageInt(hwnd As IntPtr, msg As WM) As Integer

        Return CInt(SendMessage(hwnd, CUInt(msg), 0, 0))

    End Function

    Public Shared Function GetMessageString(hwnd As IntPtr, msg As WM, param As UInteger) As String

        Dim lParam As New StringBuilder(&H10000)

        SendMessage(hwnd, msg, param, lParam)

        Return lParam.ToString()

    End Function

    <DllImport("user32.dll", CharSet:=CharSet.Auto, ExactSpelling:=True)>
    Private Shared Function GetParent(hWnd As IntPtr) As IntPtr
    End Function

    <DllImport("user32.dll")>
    Private Shared Function GetWindowRect(hWnd As IntPtr, ByRef lpRect As RECT) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function

    <DllImport("user32.dll", CharSet:=CharSet.Auto, SetLastError:=True)>
    Private Shared Function GetWindowText(hWnd As IntPtr, lpString As StringBuilder, nMaxCount As Integer) As Integer
    End Function

    <DllImport("user32.dll", CharSet:=CharSet.Auto, SetLastError:=True)>
    Private Shared Function GetWindowTextLength(hWnd As IntPtr) As Integer
    End Function

    Public Shared Function MinimizeWindow(hwnd As IntPtr) As Boolean

        Return CloseWindow(hwnd)

    End Function

    <DllImport("user32.dll", CharSet:=CharSet.Auto)>
    Private Shared Function SendMessage(hWnd As IntPtr, Msg As UInteger, wParam As IntPtr, lParam As IntPtr) As IntPtr
    End Function

    <DllImport("user32.dll", CharSet:=CharSet.Auto)>
    Private Shared Function SendMessage(hWnd As IntPtr, Msg As UInteger, wParam As UInteger, lParam As String) As IntPtr
    End Function

    <DllImport("user32.dll", CharSet:=CharSet.Auto)>
    Private Shared Function SendMessage(hWnd As IntPtr, Msg As UInteger, wParam As UInteger, lParam As StringBuilder) As IntPtr
    End Function

    <DllImport("user32.dll", CharSet:=CharSet.Auto)>
    Private Shared Function SendMessage(hWnd As IntPtr, Msg As UInteger, wParam As UInteger, lParam As UInteger) As IntPtr
    End Function

    Public Shared Function SendMessage(hwnd As IntPtr, msg As WM, param1 As UInteger, param2 As String) As Integer

        Return CInt(SendMessage(hwnd, CUInt(msg), param1, param2))

    End Function

    Public Shared Function SendMessage(hwnd As IntPtr, msg As WM, param1 As UInteger, param2 As UInteger) As Integer

        Return CInt(SendMessage(hwnd, CUInt(msg), param1, param2))

    End Function

    <DllImport("user32.dll")>
    Private Shared Function SetForegroundWindow(hWnd As IntPtr) As <MarshalAs(UnmanagedType.Bool)> Boolean
    End Function

    Public Shared Function SetHwndPos(hwnd As IntPtr, x As Integer, y As Integer) As Boolean

        Return SetWindowPos(hwnd, IntPtr.Zero, x, y, 0, 0, 5)

    End Function

    Public Shared Function SetHwndSize(hwnd As IntPtr, w As Integer, h As Integer) As Boolean

        Return SetWindowPos(hwnd, IntPtr.Zero, 0, 0, w, h, 6)

    End Function

    Public Shared Sub SetHwndText(hwnd As IntPtr, text As String)

        SendMessage(hwnd, 12, 0, text)

    End Sub

    Public Shared Function SetHwndTitle(hwnd As IntPtr, text As String) As Boolean

        Return SetWindowText(hwnd, text)

    End Function

    <DllImport("user32.dll")>
    Private Shared Function SetWindowPos(hWnd As IntPtr, hWndInsertAfter As IntPtr, X As Integer, Y As Integer, cx As Integer, cy As Integer, uFlags As UInteger) As Boolean
    End Function

    <DllImport("user32.dll")>
    Private Shared Function SetWindowText(hWnd As IntPtr, lpString As String) As Boolean
    End Function

End Class
