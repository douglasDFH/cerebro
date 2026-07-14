import React from 'react';

export default function App() {
  return (
    <div style={styles.container}>
      <h1 style={styles.title}>¡Hola Mundo!</h1>
      <p style={styles.subtitle}>Bienvenido a React con Electron para Escritorio</p>
      <p style={styles.platform}>Plataforma: {process.platform}</p>
      <p style={styles.info}>Versión de Electron: {process.versions.electron}</p>
      <p style={styles.info}>Versión de Node: {process.versions.node}</p>
    </div>
  );
}

const styles = {
  container: {
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center',
    justifyContent: 'center',
    height: '100vh',
    width: '100vw',
    backgroundColor: '#61dafb',
    margin: 0,
    padding: 0,
  },
  title: {
    fontSize: '48px',
    fontWeight: 'bold',
    color: '#282c34',
    marginBottom: '20px',
    marginTop: 0,
  },
  subtitle: {
    fontSize: '24px',
    color: '#282c34',
    marginBottom: '10px',
  },
  platform: {
    fontSize: '18px',
    color: '#282c34',
    marginTop: '20px',
    fontStyle: 'italic',
  },
  info: {
    fontSize: '16px',
    color: '#282c34',
    marginTop: '10px',
  },
};
