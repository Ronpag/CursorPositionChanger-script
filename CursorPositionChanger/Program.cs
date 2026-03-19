using System.Runtime.InteropServices;

namespace CursorPositionChanger;

class Program
{ 
    [DllImport("user32.dll")]
    static extern bool RegisterHotKey(IntPtr hWnd, int id, uint fsModifiers, uint vk);

    [DllImport("user32.dll")]
    static extern bool GetMessage(out MSG lpMsg, IntPtr hWnd, uint wMsgFilterMin, uint wMsgFilterMax);
    
    [DllImport("user32.dll")]
    static extern bool SetCursorPos(int x, int y);
    
    [DllImport("user32.dll")]
    static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, UIntPtr dwExtraInfo);
    
    const int WM_HOTKEY = 0x0312;
    
    const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
    const uint MOUSEEVENTF_LEFTUP = 0x0004;
    
    const int HK1 = 1;
    const int HK2 = 2;
    const int HK3 = 3;
    const int HK4 = 4;

    static void Main()
    {
        Console.WriteLine("Starting cursor position changer...");
        RegisterHotKey(IntPtr.Zero, HK1, 0,(uint)ConsoleKey.Tab); 
        RegisterHotKey(IntPtr.Zero, HK2, 0,(uint)ConsoleKey.Q);
        RegisterHotKey(IntPtr.Zero, HK3, 0,(uint)ConsoleKey.W);
        RegisterHotKey(IntPtr.Zero, HK4, 0,(uint)ConsoleKey.E);
        
        MSG msg;

        while (GetMessage(out msg, IntPtr.Zero, 0, 0))
        {
            if (msg.message == WM_HOTKEY)
            {
                int hotkeyId = (int)msg.wParam;

                switch (hotkeyId)
                {
                    case HK1:
                        SetCursorPos(750, 243);
                
                        Thread.Sleep(50);
                        mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, UIntPtr.Zero);
                        mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, UIntPtr.Zero);
                        break;
                    case HK2:
                        SetCursorPos(450, 427);
                
                        Thread.Sleep(50);
                        mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, UIntPtr.Zero);
                        mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, UIntPtr.Zero);
                        break;
                    case HK3:
                        SetCursorPos(690, 505);
                
                        Thread.Sleep(50);
                        mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, UIntPtr.Zero);
                        mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, UIntPtr.Zero);
                        break;
                    case HK4:
                        SetCursorPos(690, 585);
                
                        Thread.Sleep(50);
                        mouse_event(MOUSEEVENTF_LEFTDOWN, 0, 0, 0, UIntPtr.Zero);
                        mouse_event(MOUSEEVENTF_LEFTUP, 0, 0, 0, UIntPtr.Zero);
                        break;
                }
            }
        }
    }
}

public struct MSG
{
    public IntPtr hwnd;
    public uint message;
    public UIntPtr wParam;
    public IntPtr lParam;
    public uint time;
}