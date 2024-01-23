import { DisplaySideBar } from "../Sidebar.jsx"
import { MapPin, AlertCircle  } from "lucide-react"
import { useState, useEffect } from "react";
import Cadastrar from './acompanhamento/Cadastrar.jsx';
import axios from 'axios';


export function Manutencao() {
    const [itens, setItens] = useState([]);
    const backend = 'https://localhost:7116/item'

    useEffect(() => {
        const fetchData = async () => {
          try {
            const response = await axios.get(backend);
            setItens(response.data);
            console.log(response.data)
          } catch (error) {
            console.error('Erro ao obter dados do servidor:', error);
          }
        };
    
        fetchData();
      }, []);

    const estiloContainer = {
        display: 'flex',
    };
    return (
        <>
            <div style={estiloContainer}>
                <DisplaySideBar />
                <div className="w-full">
                    <SearchBar />
                    <Items items={itens} />
                </div>
            </div>

        </>
    )
}


function SearchBar() {
    return (
        <div className="flex justify-center gap-6 my-8">
            <Dropdown/>
            <input type="search" className="bg-gray-200 py-1 px3 rounde-lg shadow-md text-center w-1/2" placeholder="Buscar Item" />
            <Botao/>
        </div>
    )
}

function Botao() {
    const [mostrarCadastro, setMostrarCadastro] = useState(false);
  
    const handleClick = () => {
      setMostrarCadastro(!mostrarCadastro); // Inverte o estado atual
    };
  
    return (
      <div>
        <button onClick={handleClick} className="py-1 px-3 bg-yellow-400 rounded-lg shadow-md">Adicionar Item</button>
        {mostrarCadastro && <Cadastrar />}
      </div>
    );
  }


function Dropdown() {
    const [dropDownAberto, setDropDownAberto] = useState(false);

    function showDropdown() {
        setDropDownAberto(!dropDownAberto)
    }
   
    return (
        
        <div className="relative">
            <button onClick={showDropdown} className="py-1 px-3 bg-gray-200 rounded-lg shadow-md">Filtros</button>
            {dropDownAberto && <ul className="bg-gray-200 rounded-lg shadow-md text-center absolute top-full left-0 right-0">
            <li className="text-xs">Categoria</li>
            <li className="text-xs">Dias Conserto</li>
            <li className="text-xs">Localização</li>
            </ul>} 
        </div>
    )

}

function Items({ items }) {
    if (!items || !Array.isArray(items)) {
        return null;
    }

    const filteredItems = items.filter(item => item.status === 2);

    return (
        <section className="grid grid-cols-2 md:grid-cols-3 max-w-3xl mx-auto gap-8 p-4">
            {filteredItems.map((item, index) => (
                <div key={index} className="bg-gray-200 py-1 px-3 rounded-lg shadow-md flex flex-col gap-3">
                    <h3 className="font-bold text-lg flex items-center">
                        {item.name}
                        {item.status === 2 && <AlertCircle size={20} className="text-red-500 inline mr-2" />}
                    </h3>
                    <div className="text-xs">
                        <p>{item.description}</p>
                        <p>Unidades: {item.units}</p>
                        <p><MapPin size={20} className="inline" /> {item.location}</p>
                    </div>
                </div>
            ))}
        </section>
    );
}