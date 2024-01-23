import './index.css'
import { ToastContainer } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

/* Importanto telas */
import {Login} from './components/telas/Login'
import {Home} from './components/telas/Home'
import {Estoque} from './components/telas/Estoque'
import {Acompanhamento} from './components/telas/acompanhamento/Acompanhamento'
import {Usuario} from './components/telas/Usuario'
import {Manutencao} from './components/telas/Manutecao'

/* Importanto bibliotécas */
import {
  createBrowserRouter,
  RouterProvider
} from 'react-router-dom'

/* Criando rotas */
export default function App() {

  const router = createBrowserRouter([
    {path: "/", element: <Home />},
    {path: "/login", element: <Login />},
    {path: "/usuario", element: <Usuario />},
    {path: "/estoque", element: <Estoque />},
    {path: "/acompanhamento", element: <Acompanhamento />},
    {path: "/manutencao", element: <Manutencao />}
  ])
  return (
    <div>
      <RouterProvider router={router} />
      <ToastContainer />
    </div>
  );
}


