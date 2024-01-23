import { useState } from "react";
import { DisplaySideBar } from "../Sidebar.jsx"

export function Usuario(){
    const estiloContainer = {
        display: 'flex',
      };
    return(
        <>
        <div style={estiloContainer}>
            <DisplaySideBar/>
            <UserScreen/>
        </div>

        </>
    )
}

const styles = {
    container: {

      height: '100vh',
      display: 'flex',
      justifyContent: 'center',
      alignItems: 'center',
    },
    card: {
      backgroundColor: '#FFFFFF',
      borderRadius: '8px',
      boxShadow: '0 4px 6px rgba(0, 0, 0, 0.1)',
      overflow: 'hidden',
      width: '400px',
      maxWidth: '100%',
    },
    header: {
      backgroundColor: '#195763',
      color: '#FFFFFF',
      padding: '20px',
      textAlign: 'center',
      fontSize: '24px',
    },
    content: {
      padding: '20px',
    },
    label: {
      color: '#195763',
      fontWeight: 'bold',
      marginBottom: '5px',
      display: 'block',
    },
    info: {
      marginBottom: '20px',
    },
    button: {
      backgroundColor: '#fbcf50',
      color: '#195763',
      padding: '10px 20px',
      borderRadius: '4px',
      cursor: 'pointer',
      transition: 'background-color 0.3s',
      display: 'block',
      margin: '1rem auto',
      justify: "center",
    },
    buttonHover: {
      backgroundColor: '#e0b638',
    },
  };

  // Componente da tela de usuário
  const UserScreen = () => {
    const [isEditing, setIsEditing] = useState(false);
    const [userData, setUserData] = useState({
      name: 'John Doe',
      email: 'john.doe@example.com',
      age: 30,
    });

    const handleEditClick = () => {
      setIsEditing(true);
    };

    const handleSaveClick = () => {
      setIsEditing(false);
    };

    const handleCancelClick = () => {
      setIsEditing(false);
    };

    const handleInputChange = (e) => {
      const { name, value } = e.target;
      setUserData((prevData) => ({
        ...prevData,
        [name]: value,
      }));
    };

    return (
      <div className="w-full" style={styles.container}>
        <div style={styles.card}>
          <div style={styles.header}>Perfil do Usuário</div>
          <div style={styles.content}>
            {isEditing ? (
              <>
                <label style={styles.label}>Nome:</label>
                <input
                  style={styles.input}
                  type="text"
                  name="name"
                  value={userData.name}
                  onChange={handleInputChange}
                />

                <label style={styles.label}>Email:</label>
                <input
                  style={styles.input}
                  type="email"
                  name="email"
                  value={userData.email}
                  onChange={handleInputChange}
                />

                <label style={styles.label}>Idade:</label>
                <input
                  style={styles.input}
                  type="number"
                  name="age"
                  value={userData.age}
                  onChange={handleInputChange}
                />

                <button className="flex flex-col space-y-12" style={styles.button} onClick={handleSaveClick}>
                  Salvar
                </button>
                <button
                    className="flex flex-col space-y-12"
                  style={{ ...styles.button, ':hover': styles.buttonHover }}
                  onClick={handleCancelClick}
                >
                  Cancelar
                </button>
              </>
            ) : (
              <>
                <label style={styles.label}>Nome:</label>
                <div style={styles.info}>{userData.name}</div>

                <label style={styles.label}>Email:</label>
                <div style={styles.info}>{userData.email}</div>

                <label style={styles.label}>Idade:</label>
                <div style={styles.info}>{userData.age} anos</div>

                <button
                  style={{ ...styles.button, ':hover': styles.buttonHover }}
                  onClick={handleEditClick}
                >
                  Editar Perfil
                </button>
              </>
            )}
          </div>
        </div>
      </div>
    );
  };