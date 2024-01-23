import React, { useState } from 'react';
import axios from 'axios';
import './Cadastrar.css';
import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

// Constantes
const backend = "https://localhost:7116/ordemservico";
const statusOptions = [
  { value: 0, label: 'Não Iniciada' },
  { value: 1, label: 'Iniciada' },
  { value: 2, label: 'Atrasada' },
  { value: 3, label: 'Finalizada' },
  { value: 4, label: 'Impedida' },
];

const Cadastrar = ({ mostrarCadastro, setMostrarCadastro }) => {
  const [statusOrdemServico, setStatusOrdemServico] = useState(0);
  const [nome, setNome] = useState("");

  const postOrdemServico = (data) => {
    axios.post(backend, data)
      .then(response => {
        console.log(response.data);
        toast.success('Cadastro feito com sucesso!', {
          position: toast.POSITION.TOP_RIGHT,
          autoClose: 3000,
        });
      })
      .catch(error => {
        toast.error('Erro ao cadastrar.', {
          position: toast.POSITION.TOP_RIGHT,
          autoClose: 3000,
        });
      });
  };


  const handleStatusChange = (e) => {
    setStatusOrdemServico(parseInt(e.target.value, 10));
  };

  const registerOrdemServico = () => {
    const envio = {
      "name": nome,
      "status": statusOrdemServico,
    };

    postOrdemServico(envio);
    fecharModal();
  };

  const fecharModal = () => {
    setMostrarCadastro(false);
  };

  return (
    <div className='modal-overlay'>
      <div className="modal-content container">
        <button className="fechar-modal" onClick={fecharModal}>X</button>
        <form onSubmit={(e) => { e.preventDefault() }}>
          <div>
            <h3>Nome da Ordem De Serviço:</h3>
            <input type="text" placeholder="Nome da Ordem De Serviço" onChange={(e) => setNome(e.target.value)} />
          </div>
          <div>
            <h3>Status Da Ordem De Serviço:</h3>
            <select value={statusOrdemServico} onChange={handleStatusChange}>
              {statusOptions.map(option => (
                <option key={option.value} value={option.value}>{option.label}</option>
              ))}
            </select>
          </div>
          <div>
            <h3>Itens para Enviar:</h3>
            <input type="text" placeholder='Item para cadastrar' onChange={(e) => setNome(e.target.value)} />
          </div>
          <div>
            <button onClick={registerOrdemServico}>Cadastrar</button>
          </div>
        </form>
      </div>
    </div>
  );
};

export default Cadastrar;
