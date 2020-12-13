using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class WaveTable : MonoBehaviour
{
    // Start is called before the first frame update
    bool[,] a=new bool[45,100];
    GameObject[,] smallcube=new GameObject[45,100];   
    GameObject[,] upbutton=new GameObject[45,100];   
    GameObject[] sidebutton=new GameObject[100];   
    public int sidenum=0;
    //public GameObject DoorControllerOnTable=GameObject.Find("DoorControllerOnTables") as GameObject;    
    public GameObject DoorControllerOnTable;
    public GameObject TestButtonOnTable;
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
    void make(string str,int k=0,string str2=null,int k2=0){        	
        var size=new Vector3(0.5f,0,0.5f);
        if (k2==0)
            size = GameObject.Find(str).transform.GetComponent<Renderer>().bounds.size;                        
        var loc=GameObject.Find(str).transform.position;  
        ansxL=(int)((loc.x-size.x/2)*kx+cx);
        ansxR=(int)((loc.x+size.x/2)*kx+cx);
        anszL=(int)((loc.z-size.z/2)*kz+cz);
        anszR=(int)((loc.z+size.z/2)*kz+cz);        
        ansy=(loc.y+size.y/2)*ky+cy;	
        if (k2!=0)
            Debug.Log(ansy);
        for (int i=ansxL;i<=ansxR;i++)
            for (int j=anszL;j<=anszR;j++){                                
                if (smallcube[i,j].transform.position.y<0.35+ansy)
                    smallcube[i,j].transform.Translate(Vector3.up*(float)(0.35+ansy-smallcube[i,j].transform.position.y),Space.World);                
                if (k==1){
                    upbutton[i,j]=PrefabUtility.InstantiatePrefab(DoorControllerOnTable) as GameObject;                
                    upbutton[i,j].transform.position=new UnityEngine.Vector3((i-24)*(float)0.015+2,(float)0.701,(float)5.5+(j-49)*(float)0.015);                    
                    upbutton[i,j].transform.localScale=new UnityEngine.Vector3((float)0.015,(float)0.001,(float)0.015);
                    upbutton[i,j].transform.GetComponent<DoorController>().door=GameObject.Find(str2);
                    upbutton[i,j].SetActive(true);                                 
                    upbutton[i,j].transform.Translate(Vector3.up*(float)ansy,Space.World);                
                }            
                if (k==2){
                    upbutton[i,j]=PrefabUtility.InstantiatePrefab(TestButtonOnTable) as GameObject;                
                    upbutton[i,j].transform.position=new UnityEngine.Vector3((i-24)*(float)0.015+2,(float)0.701,(float)5.5+(j-49)*(float)0.015);     
                    upbutton[i,j].transform.localScale=new UnityEngine.Vector3((float)0.015,(float)0.001,(float)0.015);
                    upbutton[i,j].transform.GetComponent<LampStateController>().lightItem=GameObject.Find(str2);
                    upbutton[i,j].SetActive(true);             
                    upbutton[i,j].transform.Translate(Vector3.up*(float)ansy,Space.World);                    
                }
            }
        if (k==1){
            sidenum++;
            sidebutton[sidenum]=PrefabUtility.InstantiatePrefab(DoorControllerOnTable) as GameObject;                
            sidebutton[sidenum].transform.position=new UnityEngine.Vector3((ansxL-24)*(float)0.015+2-0.01f,(float)0.35,(float)5.5+(((float)(anszL+anszR))/2-49)*(float)0.015);     
            sidebutton[sidenum].transform.localScale=new UnityEngine.Vector3((float)0.001,(float)0.7,(float)0.015*(anszR-anszL+1));
            sidebutton[sidenum].transform.GetComponent<DoorController>().door=GameObject.Find(str2);
            sidebutton[sidenum].SetActive(true);             
            sidebutton[sidenum].transform.Translate(Vector3.up*(float)ansy,Space.World);  

            sidenum++;
            sidebutton[sidenum]=PrefabUtility.InstantiatePrefab(DoorControllerOnTable) as GameObject;                
            sidebutton[sidenum].transform.position=new UnityEngine.Vector3((ansxR-24)*(float)0.015+2+0.01f,(float)0.35,(float)5.5+(((float)(anszL+anszR))/2-49)*(float)0.015);     
            sidebutton[sidenum].transform.localScale=new UnityEngine.Vector3((float)0.001,(float)0.7,(float)0.015*(anszR-anszL+1));
            sidebutton[sidenum].transform.GetComponent<DoorController>().door=GameObject.Find(str2);
            sidebutton[sidenum].SetActive(true);             
            sidebutton[sidenum].transform.Translate(Vector3.up*(float)ansy,Space.World);  
            
            sidenum++;
            sidebutton[sidenum]=PrefabUtility.InstantiatePrefab(DoorControllerOnTable) as GameObject;                
            sidebutton[sidenum].transform.position=new UnityEngine.Vector3((((float)(ansxL+ansxR))/2-24)*(float)0.015+2,(float)0.35,(float)5.5+(anszL-49)*(float)0.015-0.01f);     
            sidebutton[sidenum].transform.localScale=new UnityEngine.Vector3((float)0.015*(ansxR-ansxL+1),(float)0.7,(float)0.001);
            sidebutton[sidenum].transform.GetComponent<DoorController>().door=GameObject.Find(str2);
            sidebutton[sidenum].SetActive(true);             
            sidebutton[sidenum].transform.Translate(Vector3.up*(float)ansy,Space.World);  

            sidenum++;
            sidebutton[sidenum]=PrefabUtility.InstantiatePrefab(DoorControllerOnTable) as GameObject;                
            sidebutton[sidenum].transform.position=new UnityEngine.Vector3((((float)(ansxL+ansxR))/2-24)*(float)0.015+2,(float)0.35,(float)5.5+(anszR-49)*(float)0.015+0.01f);     
            sidebutton[sidenum].transform.localScale=new UnityEngine.Vector3((float)0.015*(ansxR-ansxL+1),(float)0.7,(float)0.001);
            sidebutton[sidenum].transform.GetComponent<DoorController>().door=GameObject.Find(str2);
            sidebutton[sidenum].SetActive(true);             
            sidebutton[sidenum].transform.Translate(Vector3.up*(float)ansy,Space.World);  
        }
        if (k==2){
            sidenum++;
            sidebutton[sidenum]=PrefabUtility.InstantiatePrefab(TestButtonOnTable) as GameObject;                
            sidebutton[sidenum].transform.position=new UnityEngine.Vector3((ansxL-24)*(float)0.015+2-0.01f,(float)0.655,(float)5.5+(((float)(anszL+anszR))/2-49)*(float)0.015);     
            sidebutton[sidenum].transform.localScale=new UnityEngine.Vector3((float)0.001,(float)0.09,(float)0.015*(anszR-anszL+1));
            sidebutton[sidenum].transform.GetComponent<LampStateController>().lightItem=GameObject.Find(str2);
            sidebutton[sidenum].SetActive(true);             
            sidebutton[sidenum].transform.Translate(Vector3.up*(float)ansy,Space.World);  

            sidenum++;
            sidebutton[sidenum]=PrefabUtility.InstantiatePrefab(TestButtonOnTable) as GameObject;                
            sidebutton[sidenum].transform.position=new UnityEngine.Vector3((ansxR-24)*(float)0.015+2+0.01f,(float)0.655,(float)5.5+(((float)(anszL+anszR))/2-49)*(float)0.015);     
            sidebutton[sidenum].transform.localScale=new UnityEngine.Vector3((float)0.001,(float)0.09,(float)0.015*(anszR-anszL+1));
            sidebutton[sidenum].transform.GetComponent<LampStateController>().lightItem=GameObject.Find(str2);
            sidebutton[sidenum].SetActive(true);             
            sidebutton[sidenum].transform.Translate(Vector3.up*(float)ansy,Space.World);  
            
            sidenum++;
            sidebutton[sidenum]=PrefabUtility.InstantiatePrefab(TestButtonOnTable) as GameObject;                
            sidebutton[sidenum].transform.position=new UnityEngine.Vector3((((float)(ansxL+ansxR))/2-24)*(float)0.015+2,(float)0.655,(float)5.5+(anszL-49)*(float)0.015-0.01f);     
            sidebutton[sidenum].transform.localScale=new UnityEngine.Vector3((float)0.015*(ansxR-ansxL+1),(float)0.09,(float)0.001);
            sidebutton[sidenum].transform.GetComponent<LampStateController>().lightItem=GameObject.Find(str2);
            sidebutton[sidenum].SetActive(true);             
            sidebutton[sidenum].transform.Translate(Vector3.up*(float)ansy,Space.World);  

            sidenum++;
            sidebutton[sidenum]=PrefabUtility.InstantiatePrefab(TestButtonOnTable) as GameObject;                
            sidebutton[sidenum].transform.position=new UnityEngine.Vector3((((float)(ansxL+ansxR))/2-24)*(float)0.015+2,(float)0.655,(float)5.5+(anszR-49)*(float)0.015+0.01f);     
            sidebutton[sidenum].transform.localScale=new UnityEngine.Vector3((float)0.015*(ansxR-ansxL+1),(float)0.09,(float)0.001);
            sidebutton[sidenum].transform.GetComponent<LampStateController>().lightItem=GameObject.Find(str2);
            sidebutton[sidenum].SetActive(true);             
            sidebutton[sidenum].transform.Translate(Vector3.up*(float)ansy,Space.World);  
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
            }               
        make("bed_1/mattress");
        make("bed_1 (1)/mattress");        
        make("cabinet_1/base");
        make("kitchen_chair_1/seat"); 
        make("coffee_table_1/metal");           
        make("tv_table_1/base");
        make("table_2/base");
        make("kitchen_table_1/base");
        make("chair_3/base");
        make("chair_3 (1)/base");        
        make("Door1/DoorItem/03_low",1,"Door1/DoorItem/01_low");
        make("Door1 (1)/DoorItem/03_low",1,"Door1 (1)/DoorItem/01_low");
        make("Lamp1/torchere_1/plafond",2,"Lamp1/LightItem");        
        make("Bedside Lamp 1",2,"Bedside Lamp 1",1);
        make("Bedside Lamp 2",2,"Bedside Lamp 2",1);        
        make("Corridor Light",2,"Corridor Light",1);
        make("Toilet Light",2,"Toilet Light",1);        
    }
    void Update()
    {                           
        //this.transform.Rotate(Vector3.up*1,Space.World);                
    }
}
