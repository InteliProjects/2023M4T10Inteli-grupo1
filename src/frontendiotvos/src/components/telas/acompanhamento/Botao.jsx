import React from 'react';
import './Botao.css';

function Botao({ mostrarCadastro, setMostrarCadastro }) {
  const handleClick = () => {
    setMostrarCadastro(!mostrarCadastro);
  };

  return (
    <div>
      <button className="botao-cadastrar" onClick={handleClick}>
        Cadastrar
      </button>
    </div>
  );
}

export default Botao;