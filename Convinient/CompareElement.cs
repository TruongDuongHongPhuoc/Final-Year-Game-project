using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// yang > yin > tail > Heads > Yang   
public class CompareElement
{
    public static int Elemementreact;
    public static int Compare(Element element1, Element element2){
        
         int Elemementreact = 0;
    
    switch (element1)
    {
        case Element.yang:
            CompareYang(element2, ref Elemementreact);
            break;
        case Element.yin:
            CompareYin(element2, ref Elemementreact);
            break;
        case Element.head:
            CompareHead(element2, ref Elemementreact);
            break;
        case Element.tail:
            CompareTail(element2, ref Elemementreact);
            break;       
    }

    return Elemementreact;
    }
    public static void CompareYang(Element element2, ref int Elemementreact){
        switch(element2){
            case Element.yang:
                Elemementreact = 0;
                break;
            case Element.yin:
                Elemementreact = 1;
                break;
            case Element.head:
                Elemementreact = -1;
                break;
            case Element.tail:
                Elemementreact = 0;
                break;
        }
    }
    public static void CompareYin(Element element2,ref int Elemementreact){
        switch(element2){
            case Element.yang:
                Elemementreact = 1;
                break;
            case Element.yin:
                Elemementreact = 0;
                break;
            case Element.head:
                Elemementreact = 0;
                break;
            case Element.tail:
                Elemementreact = -1;
                break;
        }
    }
    public static void CompareTail(Element element2, ref int Elemementreact){
        switch(element2){
            case Element.yang:
                Elemementreact = 0;
                break;
            case Element.yin:
                Elemementreact = 1;
                break;
            case Element.head:
                Elemementreact = -1;
                break;
            case Element.tail:
                Elemementreact = 0;
                break;
        }
    }
    public static void CompareHead(Element element2, ref int Elemementreact){
        switch(element2){
            case Element.yang:
                Elemementreact = -1;
                break;
            case Element.yin:
                Elemementreact = 0;
                break;
            case Element.head:
                Elemementreact = 0;
                break;
            case Element.tail:
                Elemementreact = 1;
                break;
        }
    }
}
