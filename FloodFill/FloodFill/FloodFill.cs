// Copyright   Grigoriy Konovalov
// Partial (C) SLoW
using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace FloodFill
{

  // �����, � ������� ������������� ��������� ������� GDI

  public class GDI
  {
    public IntPtr CreateSolidBRUSH(uint color)
    {
      return CreateSolidBrush(color);
    }

    public bool ExtFloodFILL(IntPtr hdcSourse, int x, int y, uint �olorRefColor, uint nFillType)
    {
      return ExtFloodFill(hdcSourse, x, y, �olorRefColor, nFillType);
    }
  
    public IntPtr SelectOBJECT(IntPtr hDCSourse, IntPtr hBitmap)
    {
      return SelectObject(hDCSourse, hBitmap);
    }
  
    public IntPtr CreateCOMPATIBLEDC(IntPtr hdcSourse)
    {
      return CreateCompatibleDC(hdcSourse);
    }

    public bool DeleteOBJECT(IntPtr hObject)
    {
      return DeleteObject(hObject);
    }


    [System.Runtime.InteropServices.DllImportAttribute("gdi32.dll")]
    private static extern IntPtr CreateSolidBrush(uint crColor);

    [DllImport("gdi32", CharSet = CharSet.Auto)]
    private static extern bool ExtFloodFill(IntPtr hDC, int x, int y, uint �olorRefColor, uint nFillType);

    [System.Runtime.InteropServices.DllImportAttribute("gdi32.dll")]
    private static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);

    [DllImport("gdi32.dll")]
    private static extern IntPtr CreateCompatibleDC(IntPtr hdc);

    [System.Runtime.InteropServices.DllImportAttribute("gdi32.dll")]
    private static extern bool DeleteObject(IntPtr hObject);
  }

  public class MapFill
  {
    public MapFill()
    {
    }
    /// <summary>
    /// ������� �������
    /// </summary>
    /// <param name="g">������� ������������� ������� (��������, ������)</param>
    /// <param name="pos">�����, � ������� ���������� �������</param>    
    /// <param name="colorFill">���� �������</param>
    /// <param name="img">������, ������� ������������ �� ����� �������</param>
    public void Fill(Graphics g, Point pos, Color colorFill, ref Bitmap img)
    {       
      GDI d = new GDI();
      
      // ���� � �����, � ������� ���������� �������
      Color colorBegin = img.GetPixel(pos.X, pos.Y);

      // DC ������
      IntPtr panelDC = g.GetHdc();

      // DC � ������, ����������� � �������
      IntPtr memDC = d.CreateCOMPATIBLEDC(panelDC);

      // ������� � ����������� ���� �����
      IntPtr hBrush = d.CreateSolidBRUSH((uint)ColorTranslator.ToWin32(colorFill));
      IntPtr hOldBr = d.SelectOBJECT(memDC, hBrush);

      // ����������� ���� ������
      IntPtr hBMP = img.GetHbitmap();
      IntPtr hOldBmp = d.SelectOBJECT(memDC, hBMP);

      // �������� (���������� ��������� ������������� � �������, � ��������� ������ 
      // ������� �� ������� �� ����������)
      d.ExtFloodFILL(memDC, pos.X, pos.Y, (uint)ColorTranslator.ToWin32(colorBegin), 1);
      
      // ���������� ���������� ������� ������ � ��� ������
      img.Dispose();
      img = Bitmap.FromHbitmap(hBMP);

      // ���������� �� ����� ���������� ����� � ������
      d.SelectOBJECT(memDC, hOldBr);
      d.SelectOBJECT(memDC, hOldBmp);

      // ����������� �������������� �������
      d.DeleteOBJECT(hBMP);
      d.DeleteOBJECT(hBrush);
      d.DeleteOBJECT(memDC);      
      g.ReleaseHdc(panelDC);            
    }    
  }
}