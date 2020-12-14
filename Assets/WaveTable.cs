using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public GameObject HSVPicker;

    public GameObject locHSVPicker;
    //int hsvxL = 1, hsvxR = 5, hsvzL = 24, hsvzR = 29;
    int hsvxL = 7, hsvxR = 8, hsvzL = 16, hsvzR = 17;
    string currLabel;
    float currOffset = 0f;

    public int k=0,i=-1,j=-1;     
    public double kx,ky,kz,cx,cy,cz;
    public int ansxL,ansxR,anszL,anszR;
	public double ansy;
    public struct LightStatus
    {
        public bool on;
        public float offset;
    }
    Dictionary<string, LightStatus> dict = new Dictionary<string, LightStatus>();
    float yaxisLen = 0.345f;

    void init() {
        double roomx,roomy,roomz;
        double roomxsc,roomysc,roomzsc;    
        roomx=2.25;
        roomy=1.5;
        roomz=5;
        roomxsc=4.5;
        roomzsc=10;
        roomysc=3;        
        int WTx=15,Wtz=33;		
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

        locHSVPicker = GameObject.Instantiate(HSVPicker);
        locHSVPicker.transform.localScale = new Vector3(0.0031465f, 0.00357465f, 0.00157465f);
        //locHSVPicker.transform.localPosition = new Vector3(1.878f + 0.025f - 0.011f, 0.88f - 0.035f - yaxisLen, 5.107f);
        locHSVPicker.transform.localPosition = new Vector3(1.8f+0.232f, 0.88f - 0.035f - yaxisLen, 5f+0.47f);
        locHSVPicker.GetComponent<HSVTopSelecter>().waveTable = gameObject;
        currLabel = "";
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
                if(k != 2)
                {
                    if (smallcube[i,j].transform.position.y<0.35+ansy)
                        smallcube[i,j].transform.Translate(Vector3.up*(float)(0.35+ansy-smallcube[i,j].transform.position.y),Space.World);                
                }
                else
                {
                    smallcube[i,j].transform.Translate(Vector3.up * (float)(0.35 + dict[str].offset - smallcube[i,j].transform.position.y),Space.World); 
                }
                if (k==1){
                    upbutton[i,j]= Instantiate(DoorControllerOnTable) as GameObject;                
                    upbutton[i,j].transform.position=new UnityEngine.Vector3((i-7)*(float)0.045+1.8f,(float)0.701,(float)5.5+(j-16)*(float)0.045);                    
                    upbutton[i,j].transform.localScale=new UnityEngine.Vector3((float)0.045,(float)0.001,(float)0.045);
                    upbutton[i,j].transform.GetComponent<DoorController>().door=GameObject.Find(str2);
                    upbutton[i,j].SetActive(true);                                 
                    upbutton[i,j].transform.Translate(Vector3.up*(float)ansy,Space.World);                
                }            
                if (k==2){
                    upbutton[i,j]= Instantiate(TestButtonOnTable) as GameObject;                
                    upbutton[i,j].transform.position=new UnityEngine.Vector3((i-7)*(float)0.045+1.8f,(float)0.701,(float)5.5+(j-16)*(float)0.045);     
                    upbutton[i,j].transform.localScale=new UnityEngine.Vector3((float)0.045,(float)0.001,(float)0.045);
                    upbutton[i,j].transform.GetComponent<WaveLampStateController>().lightItem=GameObject.Find(str2);
                    upbutton[i,j].transform.GetComponent<WaveLampStateController>().waveTable = gameObject;
                    upbutton[i,j].transform.GetComponent<WaveLampStateController>().label = str;
                    upbutton[i,j].transform.GetComponent<WaveLampStateController>().xL=ansxL;
                    upbutton[i,j].transform.GetComponent<WaveLampStateController>().xR=ansxR;
                    upbutton[i,j].transform.GetComponent<WaveLampStateController>().zL=anszL;
                    upbutton[i,j].transform.GetComponent<WaveLampStateController>().zR=anszR;
                    upbutton[i,j].SetActive(true);             
                    //upbutton[i,j].transform.Translate(Vector3.up*(float)ansy,Space.World);        
                    upbutton[i,j].transform.Translate(Vector3.up * dict[str].offset,Space.World);
                }
            }
        if (k==1){
            sidenum++;
            sidebutton[sidenum]= Instantiate(DoorControllerOnTable) as GameObject;                
            sidebutton[sidenum].transform.position=new UnityEngine.Vector3((ansxL-7)*(float)0.045+1.8f-0.023f,(float)0.35,(float)5.5+(((float)(anszL+anszR))/2-16)*(float)0.045);     
            sidebutton[sidenum].transform.localScale=new UnityEngine.Vector3((float)0.001,(float)0.7,(float)0.045*(anszR-anszL+1));
            sidebutton[sidenum].transform.GetComponent<DoorController>().door=GameObject.Find(str2);
            sidebutton[sidenum].SetActive(true);             
            sidebutton[sidenum].transform.Translate(Vector3.up*(float)ansy,Space.World);  

            sidenum++;
            sidebutton[sidenum]= Instantiate(DoorControllerOnTable) as GameObject;                
            sidebutton[sidenum].transform.position=new UnityEngine.Vector3((ansxR-7)*(float)0.045+1.8f+0.023f,(float)0.35,(float)5.5+(((float)(anszL+anszR))/2-16)*(float)0.045);     
            sidebutton[sidenum].transform.localScale=new UnityEngine.Vector3((float)0.001,(float)0.7,(float)0.045*(anszR-anszL+1));
            sidebutton[sidenum].transform.GetComponent<DoorController>().door=GameObject.Find(str2);
            sidebutton[sidenum].SetActive(true);             
            sidebutton[sidenum].transform.Translate(Vector3.up*(float)ansy,Space.World);  
            
            sidenum++;
            sidebutton[sidenum]= Instantiate(DoorControllerOnTable) as GameObject;                
            sidebutton[sidenum].transform.position=new UnityEngine.Vector3((((float)(ansxL+ansxR))/2-7)*(float)0.045+1.8f,(float)0.35,(float)5.5+(anszL-16)*(float)0.045-0.023f);     
            sidebutton[sidenum].transform.localScale=new UnityEngine.Vector3((float)0.045*(ansxR-ansxL+1),(float)0.7,(float)0.001);
            sidebutton[sidenum].transform.GetComponent<DoorController>().door=GameObject.Find(str2);
            sidebutton[sidenum].SetActive(true);             
            sidebutton[sidenum].transform.Translate(Vector3.up*(float)ansy,Space.World);  

            sidenum++;
            sidebutton[sidenum]=Instantiate(DoorControllerOnTable) as GameObject;                
            sidebutton[sidenum].transform.position=new UnityEngine.Vector3((((float)(ansxL+ansxR))/2-7)*(float)0.045+1.8f,(float)0.35,(float)5.5+(anszR-16)*(float)0.045+0.023f);     
            sidebutton[sidenum].transform.localScale=new UnityEngine.Vector3((float)0.045*(ansxR-ansxL+1),(float)0.7,(float)0.001);
            sidebutton[sidenum].transform.GetComponent<DoorController>().door=GameObject.Find(str2);
            sidebutton[sidenum].SetActive(true);             
            sidebutton[sidenum].transform.Translate(Vector3.up*(float)ansy,Space.World);  
        }
        if (k==2){
            locHSVPicker.GetComponent<updateLightColor>().light = GameObject.Find(str2).GetComponent<Light>();
            /*
            sidenum++;
            sidebutton[sidenum]=PrefabUtility.InstantiatePrefab(TestButtonOnTable) as GameObject;                
            sidebutton[sidenum].transform.position=new UnityEngine.Vector3((ansxL-7)*(float)0.045+1.8f-0.023f,(float)0.655,(float)5.5+(((float)(anszL+anszR))/2-16)*(float)0.045);     
            sidebutton[sidenum].transform.localScale=new UnityEngine.Vector3((float)0.001,(float)0.09,(float)0.045*(anszR-anszL+1));
            sidebutton[sidenum].transform.GetComponent<WaveLampStateController>().lightItem=GameObject.Find(str2);
            sidebutton[sidenum].transform.GetComponent<WaveLampStateController>().waveTable = gameObject;
            sidebutton[sidenum].transform.GetComponent<WaveLampStateController>().label = str;
            sidebutton[sidenum].transform.GetComponent<WaveLampStateController>().xL=ansxL;
            sidebutton[sidenum].transform.GetComponent<WaveLampStateController>().xR=ansxR;
            sidebutton[sidenum].transform.GetComponent<WaveLampStateController>().zL=anszL;
            sidebutton[sidenum].transform.GetComponent<WaveLampStateController>().zR=anszR;
            sidebutton[sidenum].SetActive(true);             
            sidebutton[sidenum].transform.Translate(Vector3.up*(float)ansy,Space.World);  

            sidenum++;
            sidebutton[sidenum]=PrefabUtility.InstantiatePrefab(TestButtonOnTable) as GameObject;                
            sidebutton[sidenum].transform.position=new UnityEngine.Vector3((ansxR-7)*(float)0.045+1.8f+0.023f,(float)0.655,(float)5.5+(((float)(anszL+anszR))/2-16)*(float)0.045);     
            sidebutton[sidenum].transform.localScale=new UnityEngine.Vector3((float)0.001,(float)0.09,(float)0.045*(anszR-anszL+1));
            sidebutton[sidenum].transform.GetComponent<WaveLampStateController>().lightItem=GameObject.Find(str2);
            sidebutton[sidenum].transform.GetComponent<WaveLampStateController>().waveTable = gameObject;
            sidebutton[sidenum].transform.GetComponent<WaveLampStateController>().label = str;
            sidebutton[sidenum].transform.GetComponent<WaveLampStateController>().xL=ansxL;
            sidebutton[sidenum].transform.GetComponent<WaveLampStateController>().xR=ansxR;
            sidebutton[sidenum].transform.GetComponent<WaveLampStateController>().zL=anszL;
            sidebutton[sidenum].transform.GetComponent<WaveLampStateController>().zR=anszR;            
            sidebutton[sidenum].SetActive(true);             
            sidebutton[sidenum].transform.Translate(Vector3.up*(float)ansy,Space.World);  
            
            sidenum++;
            sidebutton[sidenum]=PrefabUtility.InstantiatePrefab(TestButtonOnTable) as GameObject;                
            sidebutton[sidenum].transform.position=new UnityEngine.Vector3((((float)(ansxL+ansxR))/2-7)*(float)0.045+1.8f,(float)0.655,(float)5.5+(anszL-16)*(float)0.045-0.023f);     
            sidebutton[sidenum].transform.localScale=new UnityEngine.Vector3((float)0.045*(ansxR-ansxL+1),(float)0.09,(float)0.001);
            sidebutton[sidenum].transform.GetComponent<WaveLampStateController>().lightItem=GameObject.Find(str2);
            sidebutton[sidenum].transform.GetComponent<WaveLampStateController>().waveTable = gameObject;
            sidebutton[sidenum].transform.GetComponent<WaveLampStateController>().label = str;
            sidebutton[sidenum].transform.GetComponent<WaveLampStateController>().xL=ansxL;
            sidebutton[sidenum].transform.GetComponent<WaveLampStateController>().xR=ansxR;
            sidebutton[sidenum].transform.GetComponent<WaveLampStateController>().zL=anszL;
            sidebutton[sidenum].transform.GetComponent<WaveLampStateController>().zR=anszR;
            sidebutton[sidenum].SetActive(true);             
            sidebutton[sidenum].transform.Translate(Vector3.up*(float)ansy,Space.World);  

            sidenum++;
            sidebutton[sidenum]=PrefabUtility.InstantiatePrefab(TestButtonOnTable) as GameObject;                
            sidebutton[sidenum].transform.position=new UnityEngine.Vector3((((float)(ansxL+ansxR))/2-7)*(float)0.045+1.8f,(float)0.655,(float)5.5+(anszR-16)*(float)0.045+0.023f);     
            sidebutton[sidenum].transform.localScale=new UnityEngine.Vector3((float)0.045*(ansxR-ansxL+1),(float)0.09,(float)0.001);
            sidebutton[sidenum].transform.GetComponent<WaveLampStateController>().lightItem=GameObject.Find(str2);
            sidebutton[sidenum].transform.GetComponent<WaveLampStateController>().waveTable = gameObject;
            sidebutton[sidenum].transform.GetComponent<WaveLampStateController>().label = str;
            sidebutton[sidenum].transform.GetComponent<WaveLampStateController>().xL=ansxL;
            sidebutton[sidenum].transform.GetComponent<WaveLampStateController>().xR=ansxR;
            sidebutton[sidenum].transform.GetComponent<WaveLampStateController>().zL=anszL;
            sidebutton[sidenum].transform.GetComponent<WaveLampStateController>().zR=anszR;
            sidebutton[sidenum].SetActive(true);             
            sidebutton[sidenum].transform.Translate(Vector3.up*(float)ansy,Space.World);  */
        }
        if(k == 2)
        {
            Debug.Log("light pos : xL : " + ansxL + " xR : " + ansxR + " ");
            Debug.Log("light pos : zL : " + anszL + " zR : " + anszR + " ");

        }
    }    

    int steps = 30;
    float stepTime = 0.002f;
    IEnumerator CoUpdateLight(int xL, int xR, int zL, int zR, GameObject lightItem, string label)
    {
        if(currLabel != "")
        {
            for(int i = 0; i < steps; i++)
            {
                locHSVPicker.transform.Translate( -Vector3.up * yaxisLen / steps, Space.World);
                yield return new WaitForSeconds(stepTime);
            }
        }
        locHSVPicker.GetComponent<updateLightColor>().light = lightItem.GetComponent<Light>();
        locHSVPicker.transform.localPosition += new Vector3((xL - hsvxL) * 0.045f, 0f, (zL - hsvzL) * 0.045f);
        locHSVPicker.transform.Translate(Vector3.up * (dict[label].offset - currOffset), Space.World);
        hsvxL = xL; hsvxR = xR; hsvzL = zL; hsvzR = zR;
        currLabel = label; currOffset = dict[label].offset;

        for(int i = 0; i < steps; i++)
        {
            locHSVPicker.transform.Translate(Vector3.up * yaxisLen / steps, Space.World);
            yield return new WaitForSeconds(stepTime);
        }
        
        for (int i=xL;i<=xR;i++)
            for (int j=zL;j<=zR;j++){                                
                    //smallcube[i,j].transform.Translate(Vector3.up * (float)(0.35 + dict[label].offset + 0.303 - smallcube[i,j].transform.position.y),Space.World); 
            }
        yield return 0;
    }

    public void UpdateLight(int xL, int xR, int zL, int zR, GameObject lightItem, string label)
    {
        StartCoroutine(CoUpdateLight(xL, xR, zL, zR, lightItem, label));
    }

    IEnumerator CoSelectHSVTop()
    {
        currLabel = "";
        for(int i = 0; i < steps; i++)
        {
            locHSVPicker.transform.Translate( -Vector3.up * yaxisLen / steps, Space.World);
            yield return new WaitForSeconds(stepTime);
        }
        for (int i=hsvxL;i<=hsvxR;i++)
            for (int j=hsvzL;j<=hsvzR;j++){                                
                    //smallcube[i,j].transform.Translate(Vector3.up * (float)(0.35 + dict[currLabel].offset - smallcube[i,j].transform.position.y),Space.World); 
            }  
    }

    public void SelectHSVTop()
    {
        StartCoroutine(CoSelectHSVTop());
    }

    public void SelectHSVSide()
    {
        locHSVPicker.GetComponent<updateLightColor>().light.enabled = !locHSVPicker.GetComponent<updateLightColor>().light.enabled;
    }

    void Start()
    {                     
        init();                       
        for (int i=0;i<15;i++)
            for (int j=0;j<33;j++){                
                smallcube[i,j]=GameObject.CreatePrimitive(PrimitiveType.Cube);
                smallcube[i,j].transform.localScale=new UnityEngine.Vector3((float)0.045,(float)0.7,(float)0.045);
                smallcube[i,j].transform.position=new UnityEngine.Vector3((i-7)*(float)0.045+1.8f,(float)0.35,(float)5.5+(j-16)*(float)0.045);                     
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
        LightStatus lamp; lamp.on = true; lamp.offset = 0f;
        dict["Lamp1/torchere_1/plafond"] = lamp;
        make("Lamp1/torchere_1/plafond",2,"Lamp1/LightItem");
        LightStatus b_lamp1; b_lamp1.on = true; b_lamp1.offset = 0.1f;
        dict["Bedside Lamp 1"] = b_lamp1;
        make("Bedside Lamp 1",2,"Bedside Lamp 1",1);
        LightStatus b_lamp2; b_lamp2.on = true; b_lamp2.offset = 0.1f;
        dict["Bedside Lamp 2"] = b_lamp2;
        make("Bedside Lamp 2",2,"Bedside Lamp 2",1);        
        LightStatus c_light; c_light.on = true; c_light.offset = 0f;
        dict["Corridor Light"] = c_light;
        make("Corridor Light",2,"Corridor Light",1);
        LightStatus t_light; t_light.on = true; t_light.offset = 0f;
        dict["Toilet Light"] = t_light;
        make("Toilet Light",2,"Toilet Light",1);        
    }
    void Update()
    {                           
        //this.transform.Rotate(Vector3.up*1,Space.World);                
    }
}
