import { DisplaySideBar } from "../Sidebar.jsx"

export function Home(){
    const estiloContainer = {
        display: 'flex',
      };
    return(
        <>
        <div style={estiloContainer}>
            <DisplaySideBar/>
        </div>
        
        </>
    )
}