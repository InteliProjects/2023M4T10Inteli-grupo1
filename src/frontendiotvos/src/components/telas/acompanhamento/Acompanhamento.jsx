import React, { useState, useEffect } from 'react';
import { DisplaySideBar } from "../../Sidebar";
import Botao from "./Botao";
import Cadastrar from "./Cadastrar";
import ConnectClient from "../../../servicesMqtt/ConnectClient";
import './acompanhamento.css';
import OrderCard from './OrderCard';
import axios from 'axios';

export function Acompanhamento() {
  const [mostrarCadastro, setMostrarCadastro] = useState(false);
  const { client, connectionStatus, latestMessage } = ConnectClient();
  const [ordensDeServico, setOrdensDeServico] = useState([]);

  const backend = 'https://localhost:7116/ordemservico'

  useEffect(() => {
    const fetchData = async () => {
      try {
        const response = await axios.get(backend);
        setOrdensDeServico(response.data);
        console.log(response.data)
      } catch (error) {
        console.error('Erro ao obter dados do servidor:', error);
      }
    };

    fetchData();
  }, []);

  const dadosMockados = [
    { nome: 'Ordem 1', items: 'Item A, Item B', status: 0 },
    { nome: 'Ordem 2', items: 'Item C, Item D', status: 1 },
    { nome: 'Ordem 3', items: 'Item E, Item F', status: 2 },
  ];

  return (
    <div className='container-base'>
      <DisplaySideBar />
      <div className="container">
        <Botao mostrarCadastro={mostrarCadastro} setMostrarCadastro={setMostrarCadastro} />
        <div>
          {mostrarCadastro && <Cadastrar mostrarCadastro={mostrarCadastro} setMostrarCadastro={setMostrarCadastro} />}
          {connectionStatus && <UltimaMensagem mensagem={latestMessage} />}

          {ordensDeServico.map((ordem, index) => (
            <OrderCard key={index} nome={ordem.name} items={ordem.items} status={ordem.status} />
          ))}
        </div>
      </div>
    </div>
  );
}

const UltimaMensagem = ({ mensagem }) => {
  return (
    <div className="ultima-mensagem">
      <h2>Última Mensagem:</h2>
      <p>{mensagem}</p>
    </div>
  );
};
