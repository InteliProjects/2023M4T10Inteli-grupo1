import { ChevronFirst, ChevronLast, MoreVertical } from "lucide-react";
import { createContext, useContext, useState } from "react";
import logo from "../assets_front/logo.png"
import {Package, UserCircle,Home,ClipboardIcon,Wrench,} from "lucide-react" 
import { Link } from 'react-router-dom';

const SidebarContext = createContext()

export function Sidebar({ children }) {
    const [expanded,setExpanded] = useState(true)

    const handleMouseEnter = () => {
        setExpanded(true);
      };
    
      const handleMouseLeave = () => {
        setExpanded(false);
      };
    return (
        <aside  className={`h-screen sidebar ${expanded ? 'expanded w-1/4' : 'collapsed w-1/10'}`}
        onMouseEnter={handleMouseEnter}
        onMouseLeave={handleMouseLeave} >
            <nav className="h-full flex flex-col bg-[#195763] shadow-sm">
                <div className="p-4 pb-6 flex justify-between ites-center">
                    <img src={logo}
                        className={`overflow-hidden transition-all ${expanded ? "w-32" : "hidden"}`}
                        alt=""
                    />
                    
                </div>

                <SidebarContext.Provider value={{expanded}}>
                <ul className="flex-1 px-3">{children}</ul>
                </SidebarContext.Provider>

                

                <div className="border-t flex p-3">
                    <img src=""
                        className="w-10 h-10 rounded-md"
                        alt=""
                    />
                    <div className={` 
                flex justify-between items-center
                overflow-hidden transition-all ${expanded ? "w-52 ml-3" : "w-0"} 
                
                `}>
                        <div className="leading-4">
                            <h4 className="font-semibold text-white">Sandra</h4>
                            <span className="text-xs text-white">sandra.andrade@atvos.com.br</span>
                        </div>
                        <MoreVertical size={20} className="text-white"/>
                    </div>
                </div>
            </nav>
        </aside>
    )
}


export function SidebarItem({ icon, text, active, alert, route }) {
    const {expanded} = useContext(SidebarContext)
    return (
        <Link to={route}>
        <li className={`
            relative flex items-center py-2 px-3 my-1
            font-medium rounded-md cursor-pointer
            transition-colors group
            ${
                active
                ? "bg-gradient-to-tr from-indigo-200 to-indigo-100 text-[#195763]" 
                : "hover:bg-[#A2B3C8] text-white"
            }
        `}>
            {icon}
            <span 
            
            className={`overflow-hidden transition-all ${
                expanded ? "w-52 ml-3" : "hidden"
            }`}
            
            >
                
                {text}</span>

            {alert && (
                <div className={`absolute right-2 w-2 h-2 rounded  bg-yellow-400 ${ //Fazer código para a bolinha ficar na página que você está alocado
                    expanded ? "" : "center"
                }`}/>
            )}

            {!expanded && <div className={` 
                absolute left-full rounded-md px-2 py-1 ml-6  
                bg-yellow-100 text-[#195763] text-sm
                invisible opacity-20 -translate-x-3 transition-all
                group-hover:visible group-hover:opacity-100 group-hover:translate-x-0 
            `}>{text}</div>}
        </li>
    </Link>
    )
}

export function DisplaySideBar(){
    return(
        <Sidebar>
        <SidebarItem icon={<Home size={20} />} text="Início" alert route="/" />
        <SidebarItem icon={<UserCircle size={20} />} text="Administrador" route="/usuario"/>
        <SidebarItem icon={<Package size={20} />} text="Estoque" route="/estoque"/>
        <SidebarItem icon={<ClipboardIcon size={20} />} text="Ordem de Serviço" route="/acompanhamento"/>
        <SidebarItem icon={<Wrench size={20} />} text="Manutenção" route="/manutencao"/>
        </Sidebar>
    )
}

