//c:\Windows\Microsoft.NET\Framework\v4.0.30319\csc.exe /reference:itext.kernel.dll;itext.io.dll;Common.Logging.dll;BouncyCastle.Crypto.dll myChangeBookmarks_v1.cs

using System;
using System.Collections.Generic;
using System.IO;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Navigation;

public class myChangeBookmarks {

public static string SRC_PDF = "";
public static string DEST_PDF = "";
public static string LIST_TXT = "";

/////
public static void Main(String[] args)
{

//---------------------
SRC_PDF = @"./" + args[0];
System.IO.FileInfo fi0 = new System.IO.FileInfo(SRC_PDF);
if(!fi0.Exists){
Console.WriteLine("File is not exists! {0}", SRC_PDF);
return;
}

//---------------------
DEST_PDF = @"./" + args[1];
System.IO.FileInfo fi1 = new FileInfo(DEST_PDF);
fi1.Directory.Create();

//---------------------
LIST_TXT = @"./" + args[2];
System.IO.FileInfo fi2 = new System.IO.FileInfo(LIST_TXT);
if(!fi2.Exists){
Console.WriteLine("File is not exists! {0}", LIST_TXT);
return;
}

PdfDocument pdfDoc = new PdfDocument(new PdfReader(SRC_PDF), new PdfWriter(DEST_PDF));
pdfDoc.GetOutlines(true).RemoveOutline();

using (System.IO.StreamReader sr = new System.IO.StreamReader(LIST_TXT, System.Text.Encoding.UTF8))
{
int line_cnt = 0;
string line = "";
while ((line = sr.ReadLine()) != null){
string[] p = line.Split('\t');
int i = int.Parse(p[1]);
PdfOutline outline = pdfDoc.GetOutlines(true).AddOutline(p[0],line_cnt);
outline.AddDestination(PdfExplicitDestination.CreateFit(pdfDoc.GetPage(i)));
line_cnt++;
}
}

pdfDoc.Close();
Console.WriteLine("== Done ==");

}
/////

}