using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveTable : MonoBehaviour
{
    // Start is called before the first frame update
    bool[,] a=new bool[45,100];
    GameObject[,] smallcube=new GameObject[45,100];   
    public int k=0,i=-1,j=-1;     
    public double kx,ky,kz,cx,cy,cz;                         
    public int ansxL,ansxR,anszL,anszR;
	public double ansy;
    void init() {
        double roomx,roomy,roomz;
        double roomxsc,roomysc,roomzsc;    
        roomx=2.25;
        roomy=1.5;
        roomz=5;
        roomxsc=4.5;
        roomzsc=10;
        roomysc=3;        
        int WTx=45,Wtz=100;		
        double WTy=0.7;        
        double x1=roomx-roomxsc/2;
        double x2=roomx+roomxsc/2;
        double z1=roomz-roomzsc/2;
        double z2=roomz+roomzsc/2;
        double y1=roomy-roomysc/2;
        double y2=roomy+roomysc/2;
        double X1=0;
        double X2=WTx;
        double Y1=0;
        double Y2=WTy;
        double Z1=0;
        double Z2=Wtz;    
        kx=(X1-X2)/(x1-x2);
        cx=X2-x2*(X1-X2)/(x1-x2);
        kz=(Z1-Z2)/(z1-z2);
        cz=Z2-z2*(Z1-Z2)/(z1-z2);
        ky=(Y1-Y2)/(y1-y2);
        cy=Y2-y2*(Y1-Y2)/(y1-y2);	
    }
    void make(string str){	
        var size = GameObject.Find(str).transform.GetComponent<Renderer>().bounds.size;
        var loc=GameObject.Find(str).transform.position;  
        ansxL=(int)((loc.x-size.x/2)*kx+cx);
        ansxR=(int)((loc.x+size.x/2)*kx+cx);
        anszL=(int)((loc.z-size.z/2)*kz+cz);
        anszR=(int)((loc.z+size.z/2)*kz+cz);
        ansy=(loc.y+size.y/2)*ky+cy;	
        for (int i=ansxL;i<=ansxR;i++)
        for (int j=anszL;j<=anszR;j++){
            smallcube[i,j].transform.Translate(Vector3.up*(float)ansy,Space.World);
        }
    }    
    void Start()
    {             
        init();      
        for (int i=0;i<45;i++)
            for (int j=0;j<100;j++){                
                smallcube[i,j]=GameObject.CreatePrimitive(PrimitiveType.Cube);
                smallcube[i,j].transform.localScale=new UnityEngine.Vector3((float)0.015,(float)0.7,(float)0.015);
                smallcube[i,j].transform.position=new UnityEngine.Vector3((i-24)*(float)0.015+2,(float)0.35,(float)5.5+(j-49)*(float)0.015);
                smallcube[i,j].GetComponent<MeshRenderer>().material.color =new Color((float)i/44,(float)j/99,0,1);
                smallcube[i,j].SetActive(true);  
                a[i,j]=false;              
            }               
        make("bed_1/base");
        make("bed_1 (1)/base");
        make("cabinet_1/base");
        make("kitchen_chair_1/seat");
    }

    public string gett(){
        return "测试结果："+k.ToString()+" "+i.ToString()+" "+j.ToString();
    }
    int geti(){
        return i;
    }

    // Update is called once per frame
    void change(int i,int j){        
        a[i,j]=!a[i,j];
        if (a[i,j]){
            smallcube[i,j].transform.Translate(Vector3.up*1f,Space.World);
        }else{
            smallcube[i,j].transform.Translate(Vector3.down*1f,Space.World);
        }
    }
    void Update()
    {                           
        //this.transform.Rotate(Vector3.up*1,Space.World);                
        if (Input.GetKeyDown("0")){
            k+=1;
            if (k==1){                
                i=0;
            }else{                
                j=0;
                change(i,j);
                k=0;
            }
        }
        if (Input.GetKeyDown("1")){
            k+=1;
            if (k==1){
                i=1;
            }else{
                j=1;
                change(i,j);
                k=0;
            }
        }
        if (Input.GetKeyDown("2")){
            k+=1;
            if (k==1){
                i=2;
            }else{
                j=2;
                change(i,j);
                k=0;
            }
        }
        if (Input.GetKeyDown("3")){
            k+=1;
            if (k==1){
                i=3;
            }else{
                j=3;
                change(i,j);
                k=0;
            }
        }
        if (Input.GetKeyDown("4")){
            k+=1;
            if (k==1){
                i=4;
            }else{
                j=4;
                change(i,j);
                k=0;
            }
        }    
        if (Input.GetKeyDown("escape")){
            if (k==1)k-=1;
        }
    }
}
