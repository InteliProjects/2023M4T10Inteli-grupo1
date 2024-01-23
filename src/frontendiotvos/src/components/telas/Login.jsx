import { useState } from "react";
import axios from 'axios';
import iotvosLogo from "../imagens/iotvosLogo.png"

const backend = "https://localhost:7116/authentication/login";

export function Login(){
    function userLogin(e){
        e.preventDefault();
        axios.post(backend, {
            email: email,
            password: password
        })
        .then(response => {
            const token = response.data.token;
            localStorage.setItem('token', token);
            console.log(response.data);
        })
        .catch(error => {
            console.error('Erro ao fazer login:', error);
        });
    }

    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');


    return(
        <div className="w-screen h-screen flex flex-row bg-black">

            <div className=" w-screen p-10 bg-green-iotvos flex flex-col gap-4 items-center justify-center">
                    <img src={iotvosLogo} class="w-60"></img>
                    <h2 className="text-yellow-iotvos text-center font-inter font-bold text-4xl">BEM VINDO!</h2>
                    <p className="text-yellow-iotvos text-center font-inter">"Juntos pelo mesmo propósito de transformar vidas"</p>
            </div>

            <div className="w-screen p-10 bg-gray-100 flex flex-col gap-4 items-center justify-center">

                <h2 className="text-green-iotvos text-center text-shadow font-inter font-bold text-4xl leading-normal">ENTRE NA SUA CONTA</h2>
                <p className="text-green-iotvos text-center font-inter font-semibold text-lg">Faça login para ter todo acesso a plataforma IoTvos</p>

                <form onSubmit={userLogin} className="w-96 flex flex-col gap-4">
                        <div className="flex flex-col bg-gray-iotvos p-2 rounded-md shadow">

                            <label className="font-inter text-green-iotvos" htmlFor="email">Usuário</label>
                            <input type="text" id="email" name= "email"className="bg-inherit" onChange={(e)=>setEmail(e.target.value)}/>
                        </div>

                        <div className="flex flex-col bg-gray-iotvos p-2 rounded-md shadow">

                            <label className="font-inter text-green-iotvos" htmlFor="password">Senha</label>
                            <input type="password" id="password" name="password" className="bg-inherit" onChange={(e)=>setPassword(e.target.value)}/>
                        </div>

                        <div className="flex flex-col items-center">
                            <input type="submit" value="Entrar" className="w-44 py-2 bg-yellow-iotvos hover:bg-yellow-500 text-white font-bold rounded text-center "/>
                        </div>
                </form>
            </div>
        </div>
    )
}