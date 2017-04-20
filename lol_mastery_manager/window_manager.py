import win32con
import win32ui
import win32gui


def find_window(window_name):
    return win32ui.FindWindow(None, window_name)


def get_hwnd(window):
    return window.GetSafeHwnd()


def activate_window(window):
    window.ShowWindow(win32con.SW_RESTORE)
    window.SetForegroundWindow()


def get_size(window):
    hwnd = get_hwnd(window)
    return win32gui.GetWindowRect(hwnd)
