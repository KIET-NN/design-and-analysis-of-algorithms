# import scrapy
import pandas as pd
import networkx as nx
import matplotlib.pyplot as plt
from io import StringIO

data = """route,stops
U-1,manzoor colony>100 ft road>azam basti>corporation gate>kalapul>jinnah hospitall>luck star hotel>mansfield street>empress markett>regal>tibbat centre>m.a jinnah road>jama cloth>city court>juna market>lee market>kakri ground>masan road>agra taj colony>rasheedabad
D-10,korangi 100 quarter>korangi no 5>landhi no 6>landhi no 5>landhi no 4>landhi no 3>landhi no 2>landhi no 1>dawood chowrangi>quaidabad>malir city>malir halt>star gate>natha khan goth>drigh road>rashid minhas road>drive-in-cinema>jauhar mor>NIPA chowrangi>gulshan chowrangi>sohrab goth>power house>godhra>new Karachi industrial area>saba cinema>new Karachi no 5>allahwali masjid>4-k chowrangi>kda stop>maraqba hall>khuda ki basti>lerp colony>taiser town
F-16,mianwali colony>qasba colony>banaras chowk>abdullah college>paposh nagar>abbasi shaheed hospital>nazimabad no 7>mujahid colony>dr. ziauddin hospital>karimabad>liaquatabad no 10>teen hatti>mazar-e-quaid>khudadad colony>nursery>ftc bulding>defence>akhter colony>qayyumabad>korangi no 1>korangi no 2 ?-> korangi no 3>korangi no 4>korangi market no 6>degree college>100 quarter>korangi no 5 ?-> landhi no 6>landhi no 1>dawood chowrangi>gul ahmed textile mills>labour square>machine tool factory
F-17,ziaul haq colony>mominabad>bijli nagar>orangi no 4>orangi no 5>metro cinema>banaras chowk>abdullah college>paposh nagar>petrol pump>liaquatabad no 10>teen hatti>gurumandir>numaish>shahrah-e-quaideen>nursery>gora qabrastan>kalapul>defence society>akhter colony>qayyumabad>chamra chowrangi>korangi industrial area>bilal colony>bismillah stop>korangi no 4>korangi no 5 ?-> dhobi ghat>100 quarter>banalipara
S-2,korangi 100 quarter>chungi naka coast guad>korangi no 2 ?-> korangi no 1>korangi crossing>qayyumabad>defence view>baloch colony>tariq road>shaheed-e-millat road>jail road>purani sabzi mandi>hassan square>NIPA chowrangi>rashid minhas>mahal city garden>sohrab goth>shahrah waliullah road>power house>shafiq mor>godhra chowk>industrial area>godhra camp>saba cinema>ayub goth
F-1,saudabad>malir jinnah square>malir city>quaidabad>dawood chowrangi>17 bus stop>babar market>landhi no 5>korangi no 5>korangi no 1>nasir colony
G-13,fareed colony>19-d>naiabadi>rasheedabad>rasheed textile mills>v.f road>banaras chowk>abdullah college>board office>hyderi>chowrangi>ziauddin hospital>moosa colony>liaquatabad no 10>gharibabad>hassan square>university of karachi
20,clifton>mohata palace>2 talwar>schon chowrangi>3 talwar>clifton bridge>bagh jinnah>shaheen complex>lucky star>Saddar>garden>lasbela chowk>old golimar>bara board>habib bank>valika>site police station>labour square>hub river road>sawat colony>police training school>ruby cinema>saeedabad naiabadi sector-8
X-25,moach goth>shershah>pankha hotel>mira naka mirza adam khan road>agra taj hanif manzil>kharadar>ismail jamat khana>tower>keamari>tarachand road>kpt ground>shirin jinnah colony>kharkar chowrangi>clifton>26th street
X-8,ittehad town>dawood goth>naval colony>moach goth>hub river road>shershah>gulbai>mari pure road>ici bridge>kardar>tower>keamari>masan road>shirin jinnah colony>shahrah firouse mazar abdullah shah gazi>26th street>khyaban shamsher>saba avenue>khyaban ittehad>qayyumabad
C-3,moosa colony>azam square>liaquatabad no 10>hassan square>gulshan-e-iqbal>purani sabzi mandi>new town police station>national stadium>dalimia cement factory>drigh road>malir>quaidabad>landhi no 6>korangi no 5>k- area>36-b area>ground landhi
F-13,pahalwan goth>al shifa terminal no 4>malir halt>quaidabad>dawood chowrangi>landhi no 4>36-b area>mills area>bilal colony>kalapul>jinnah hospitall>cantt station>gizri
F-9,gul ahmed textile mills>dawood chowrangi>89 bus stop>babar market>chirgh hotel>landhi no 6>36-b auliya masjid burmi colony>landhi k-area>korangi i-area>korangi no 5>khudadad colony>M.A Jinnah road>gurumandir>jahangir road>teen hatti>liaquatabad no 10>k.v seciety hospital>site pathan colony>abdul hameed badayuni college
C-2,raja ghazanfar ali road>inverarity road>lucky star>jinnah hospitall>kalapul>defence society>akhter colony>qayyumabad>bhittai colony>korangi bus depot>korangi no 1>korangi no 5>36-c area>k- area>landhi no 5>gul ahmed textile mills
D-4,taiser town>khuda ki basti>surjani sector-4>kda site office>two minutes chowrangi>4-j bus stop>khawaj ajmeri nagri>sector no 7-d>anda mor>qalandari chowk>hussain d.silva>paposh nagar>bara maidan>nazimabad petrol pump>gulbahar>lasbela>garden>ranchore line>juna market>lee market>gulbai>shershah>hub river road>moach mor>no 9>baldia yousuf goth
1-E,manzoor colony>bismillah pul>mehmoodabad no 6>mehmoodabad no.5>mehmoodabad no.4>mehmoodabad no.3>mehmoodabad no.2>mehmoodabad no.1>corporation>parsi colony>kalapul>jinnah hospitall>regent palaz>lucky star>Saddar>numaish>gurumandir>patel para>lasbela>golimar>petrol pump>nazimabad>habib bank>valika motrovil>orangi no 4>orangi no 10>faqeer colony
1-L,raees amrohi colony>islam chowk>nishan-e-hyder chowk>orangi no 5>metro bamarag>habib bank>bara road>bismillah hotel>garden>Saddar>jinnah hospital>akhter colony>qayyumabad>korangi industrial area>sharifabad
11-A,gulistan-e-jouhar>sufari park>NIPA>civic centre>sabzi mandi>jail road>hyderabad colony>gurumandir>numaish>Saddar>regal>saeed manzil>M.A Jinnah road
11-C,azam basti>corporation>parsi colony>kalapul>jinnah hospitall>Saddar>7th day hospital>numaish>gurumandir>islamia college>jail chowrangi>new town police station>sabzi mandi>civic centre>urdu college>NIPA>safari park>Karachi university>safoora goth>saadi town
17-H,Saddar>jinnah central hospital>kalapul>defence market>akhter colony>kpt flyover interchange>korangi crossing>korangi no 1>korangi no 2-1/2>korangi no 4>korangi no 5-1/2>dhobi ghat
17-I,ghouspak>coast guad>korangi no 2>nasir colony>chamra chowrangi>brookse chowrangi>kpt flyover interchange>akhter colony>defence mor>kalapul>jinnah hospitall>lucky star>Saddar>numaish>gurumandir>islamia college>jail chowrangi>new town>agha khan hospital>national stadium>drive-in-cinema>aladin park>NIPA chowrangi>kala board>safari park>jauhar chowrangi>pahalwan goth
17-K,khadda quaters>hospital chorangi>market no 6>gul ahmed textile mills>tasweer mahal>quaidabad>dhobi ghat>dawood chowrangi>mansehra colony>korangi no 6>qayyumabad>murtaza chowrangi>defence society>singer chowrangi>jinnah hospitall>bilal chowrangi>raja ghazanfar ali road>vita chowrangi>chamra chowrangi>brookse chowrangi>kpt flyover interchange>akhter colony>defence mor>kalapul>jinnah hospitall>lucky star>Saddar
2-C,rehmania mour>khawaja ajmeer nagri>baradari>disco mor>nusrat bhutto colony>abdullah college>nazimabad no 7>petrol pump>lasbela>patel para>gurumandir>numaish>Saddar>frere road>jama cloth market>tower
2-D,f.b area>noorani masjid>peoples colony>imam bara>humayun road>k.d.a office>north nazimabad>board office>nazimabad no 7>petrol pump>lasbela>sabil wali masjid>soldier bazar>Saddar>frere road>m.w tower>noorani masjid
2-K,north nazimabad block m>shahra-e-jehangir>water pump>hyderi>paposh nagar>bara maidan>nazimabad no 2>petrol pump>gurumandir>Saddar>burns road>tower>keamari
4-B,new Karachi>industrial area>karimabad>liaquatabad>teen hatti>gurumandir>M.A Jinnah road>bandar road>preedy street>police station>Saddar
4-G,naiabadi>karella mour>power house>saleem centre>u.p mour>nagan chowrangi>sakhi hassan>hyderi>board office>petrol pump>liaquatabad no 10>post office>teen hatti>gurumandir>numaish>Saddar>empress markett>frere road>burns road>M.A Jinnah road>tower
4-L,maymar complex>al-asif plaza>sohrab goth>water pump>karimabad>liaquatabad>teen hatti>numaish>Saddar>empress markett>burns road>m.a jinnah road>tower
6-B,tower>light house>burns road>Saddar>M.A Jinnah road>gurumandir>lasbela>golimar>nazimabad chowrangi>petrol pump>liaquatabad no 10>karimabad>hussainabad>azizabad no.3>azizabad no.4>azizabad no.8>dastagir>govt.comprehonsive school>yasinabad>hussainabad>karimabad>liaquatabad no 10>petrol pump>nazimabad chowrangi>golimar>gurumandir>M.A Jinnah road>Saddar>burns road>light house>tower
61-A,pakistan refinery>crossing>qayyumabad>akhter colony>defence mor>kalapul>jinnah hospitall>lucky star>Saddar
C,Saddar>lucky star>jinnah hospitall>kalapul>korangi road>akhter colony>qayyumabad>korangi crossing>korangi no 1>korangi no 2>korangi no 2?-> korangi no 3>korangi no 4>korangi no 5>korangi no 5-1/2>korangi market no 6>dhobi ghat
17-I,ghouspak>coast guad>korangi no 2>nasir colony>chamra chowrangi>brookse chowrangi>kpt flyover interchange>akhter colony>defence mor>kalapul>jinnah hospitall>lucky star>Saddar>numaish>gurumandir>islamia college>jail chowrangi>new town>agha khan hospital>national stadium>drive-in-cinema>aladin park>NIPA chowrangi>kala board>safari park>jauhar chowrangi>pahalwan goth
45-A,khokrapar no 4>saudabad>liaquat market>jinnah square>mohabbat nagar>malir no 15>star gate>drigh road station>drive-in-cinema>NIPA chowrangi>hassan square>nazimabad>petrol pump>habib bank>naurus chowrangi>ghani chowrangi>shershah hub river road
51-C,chamra chowrangi>vita chowrangi>bilal chowrangi>singer chowrangi>murtaza chowrangi>dawood chowrangi>quaidabad>murghi khana malir>malir no 15>kala board malir>malir halt>jinnah airport entrance>star gate>drigh road station>millenium mall>jauhar mor>aladin park>NIPA>gulshan-e-iqbal chowrangi>moti mehal>fazal mills>sohrab goth>al asif square>khuda ki basti>new sabzi mandi
7-H,keamari>tower>bolton market>eidgah>ranchore line>garden>lasbela>postoffice>sindhi hotel>new town>civic centre>NIPA>university road>safoora goth>tokyo terrace plaza
7-K,safoora goth>pia society>pahalwan goth>jauhar chowrangi>aladin park>NIPA chowrangi>gulshan chowrangi>sohrab goth>power house>nagan chowrangi>sakhi hassan>hyderi>kda cowrangi>petrol pump>habib bank>naurus chowrangi>ghani chowrangi>shershah>gulbai>paf gate>masroor>new truck stand grace village
D-10,korangi 100 quarter>korangi no 5>landhi no 6>landhi no 5>landhi no 4>landhi no 3>landhi no 2>landhi no 1>dawood chowrangi>quaidabad>malir city>malir halt>star gate>natha khan goth>drigh road>rashid minhas road>drive-in-cinema>jauhar mor>NIPA chowrangi>gulshan chowrangi>sohrab goth>power house>godhra>new Karachi industrial area>saba cinema>new Karachi no 5>allahwali masjid>4-k chowrangi>kda stop>maraqba hall>khuda ki basti>lerp colony>taiser town
D-12,qaddafi town>quaidabad>malir halt>star gate>drigh road>labour campuse>rashid minhas road>NIPA chowrangi>gulshan moti mehal>sohrab goth>water pump>qalandari chowk>gulshan farooq>qabrastan
D-19,filter plant>saadi town>safoora goth>mosmayat>university road>safari park>NIPA chowrangi>kashmir chowrangi>islamia college>gurumandir>sabil wali masjid>numaish>tibbat centre>saeed manzil>jama cloth market>light house>denso hall>tower
F-11,gul ahmed textile mills>dawood chowrangi>landhi no 1>korangi no 6>korangi no 1>defence society>gora qabrastan>shahrah quaideen>khalid bin waleed road>shaheed-e-millat road>jail chowrangi>purani sabzi mandi>university road>hassan square>NIPA chowrangi>kmc hospital>water pump>peoples chowrangi>d.c office>sakhi hassan>qalandari chowk>mustafabad colony
F-18,biscuit factory>chamra chowrangi>bilal chowrangi>bilal colony>korangi telephone exchange>korangi 2 ?-> double road>korangi no 3>korangi no 6>landhi no 6>dawood chowrangi>quaidabad>malir no 15>star gate>natha khan goth>shaheed rashid minhas road>NIPA chowrangi>gulshan chowrangi>sohrab goth>power house>nagan chowrangi>u.p mour>salim square>two minutes chowrangi
G-10,orangi town>zia chowk>iqbal market>orangi islam chowk>orangi no 5>banaras chowk>habib bank>petrol pump>nazimabad>liaquatabad no 10>karimabad>water pump>sohrab goth>NIPA chowrangi>gulshan-e-iqbal>safari park>rabia city>pahalwan goth
G-12,yaqoobabad orangi town>mansoor nagar>rehmat chowk>iqbal market>islam chowk>nishan-e-haider road>bacha khan chowk>paposh nagar>nazimabad no 1>nazimabad no 2>petrol pump>liaquatabad no 10>hassan square>urdu science college>NIPA chowrangi>safari park>jauhar square>pahalwan goth
G-21,pahalwan goth>safari park>university road>NIPA chowrangi>hassan square>jail road>shaheed-e-millat road>baloch colony>new bund chowrangi>bilal chowrangi>hassan chowrangi>landhi no 4>babar market>hospital chorangi>bhains colony
G-25,ittehad town>gulistan-e-jouhar>jauhar square>drive-in-cinema>NIPA chowrangi>samdani hospital>yasinabad>aisha manzil>landi kotal five star>north nazimabad>hyderi>board office>abdullah college>banaras chowk>orangi no 4>badar hotel>bejili nager>mominabad>rasheedabad
G-27,orangi town>yousuf goth>hub river road>bukhramandi>afridi chowk>didgah mor>jamia mehnodia>orangi town>saddiq akber colony>sabri chowk>urdu chowk>orangi no 10>orangi no 4>orangi no 5>metro cinema>banaras chowk>abdullah college>board office>kda chowrangi>ziauddin hospital>landi kotal>peoples chowrangi>buffer zone>power house>sohrab goth>fazal mills>NIPA chowrangi>safari park>jauhar square>pahalwan goth>pia society>bakhtawar goth>gul city>safoora goth>miran hospital
G-55,ittehad town>dawood goth>naval colony>moach goth>mohajir camp>hub river road>rasheedabad>labour square>ghani chowrangi>bara board>old golimar>garden>soldier bazar>gurumandir>islamia college>sharifabad t.v station>agha khan hospital>national stadium>dalmia road>rashid minhas road>NIPA chowrangi>safari park>university road>suparco chowk>sachal goth>mohammad khan goth
G-8,dhabi bux goth>rustam goth>royal stop>jauhar square>safari park>NIPA chowrangi>aziz bhatti park>kda civic centre>purani sabzi mandi>jamshed road>gurumandir>7th day hospital>M.A Jinnah road>city court>juna market>lee market>kharadar>jamat khana>keamari>dak khana masan road>shirin jinnah colony
G-9,naiabadi>qabrastan>mohajir camp>shershah>mira naka>chakiwara>lee market>siddiq wahab road>old haji camp>lawrence road>garden>lasbela>teen hatti>Karachi central jail>purani sabzi mandi>hassan square>NIPA chowrangi>safari park>gulshan jauhar goth
S-3,ahsanabad>maymar complex>al asif square>sohrab goth>NIPA chowrangi>civic centre>agha khan hospital>jail chowrangi>shaheed-e-millat road>baloch colony>korangi no 3>akbar shah goth>ibrahim hyderi
X-20,saadi town>gulshan umar>race course>safoora goth>Karachi university>NIPA chowrangi>hassan square>jail road>plaza>ramsawami>shershah ghani chowrangi>labour square>rasheedabad>chandni chowk>moach colony>qasim ali shah
19-D,faqeer colony>ruby cinema>hub river road>shershah>chakiwara>lee market>haji camp>jamila street>nishter road>baba urdu road>shahrah-e-liaquat>fresco chowk>mohammad bin qasim road>m.a jinnah road>mansfield street>empress markett>mir karim ali talpur roadd>wellington street>cantt station>clifton>dehli colony>gizri>zamzaman neelam colony>abdullah shah ghazi>khyaban saadi>bilawal chowrangi>kharkar chowrangi
4-Q,faqeer colony>ruby cinema>hub river road>shershah>chakiwara>lee market>haji camp>jamila street>nishter road>baba urdu road>shahrah-e-liaquat>fresco chowk>mohammad bin qasim road>m.a jinnah road>mansfield street>empress market>mir karim ali talpur road>wellington street>cantt station>clifton>dehli colony>gizri>zamzaman neelam colony>abdullah shah ghazi>khyaban saadi>bilawal chowrangi>kharkar chowrangi
45-A,khokrapar no 4>saudabad>liaquat market>jinnah square>mohabbat nagar>malir no 15>star gate>drigh road station>drive-in-cinema>n.i.p.a chowrangi>hassan square>nazimabad>petrol pump>habib bank>naurus chowrangi>ghani chowrangi>shershah hub river road
P-4,hub river road>shershah>gulbai>wazir mansion>west wharf road>dockyard>khoja jamat khana>tower>city post office>jama cloth market>m.a jinnah road>saeed manzil>mazar-e-quaid>shahrah-e-quaideen>khalid bin waleed road>shaheed-e-millat road>punjab colony>schon circle>boat basin>bilal chowrangi>shirin jinnah colony
P-9,memon goth>jama goth>mulla issa old thena bux vill>asso goth>dond goth>shahrah faisal>drigh road>dalimia cement factory>national stadium>jail chowrangi>p.i.b colony>teen hatti>lasbela chowk>garden>shoe market>ranchore line>haji camp>lee market>chakiwara>shershah>rcd high way hub river road"""

df = pd.read_csv(StringIO(data))
G = nx.Graph()
for _, row in df.iterrows():
    route = row['route']
    # Exclude routes which do not include neither korangi crossing nor water pump
    if "korangi" in row['stops'] or "liaquatabad" in row['stops']:
        stops = row['stops'].split('>')    
        for i in range(len(stops) - 1):
            G.add_edge(stops[i], stops[i + 1], route=route)

# print("Nodes:", G.nodes())
# print("Edges:", G.edges())

degrees = dict(G.degree())
for node, degree in degrees.items():
    if degree <= 3 or degree >= 10:
        print(f"Node {node} has {degree} neighbours.")
        G.remove_node(node)

connected_components = list(nx.connected_components(G))
G = G.subgraph(max(connected_components, key=len))

# Visualizing the graph
pos = nx.random_layout(G)  # spring_layout kamada_kawai_layout, random_layout, shell_layout
labels = nx.get_edge_attributes(G, 'route')
nx.draw(G, pos, with_labels=True, font_size=6, node_size=1, node_color='skyblue', font_color='black', edge_color='gray')
nx.draw_networkx_edge_labels(G, pos, edge_labels=labels)

plt.title("Bus Stops and Routes")
plt.show()
