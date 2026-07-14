import React from 'react';

export default function App() {
  const teamMembers = [
    'Jhoel Cruz Zurita',
    'Leonardo Peña Añez',
    'Douglas Flor Hernandez',
    'Luis Mario Rocha Vela'
  ];

  return (
    <div style={styles.container}>
      <div style={styles.card}>
        <div style={styles.header}>
          <h1 style={styles.title}>¡Hola Mundo!</h1>
          <div style={styles.badge}>Electron + React</div>
        </div>

        <div style={styles.teamSection}>
          <h2 style={styles.teamTitle}>Grupo Dinamita</h2>
          <div style={styles.membersGrid}>
            {teamMembers.map((member, index) => (
              <div key={index} style={styles.memberCard}>
                <div style={styles.memberIcon}>👤</div>
                <span style={styles.memberName}>{member}</span>
              </div>
            ))}
          </div>
        </div>

        <p style={styles.subtitle}>Bienvenido a React con Electron para Escritorio</p>

        <div style={styles.infoSection}>
          <div style={styles.infoCard}>
            <span style={styles.infoLabel}>Plataforma</span>
            <span style={styles.infoValue}>{process.platform}</span>
          </div>
          <div style={styles.infoCard}>
            <span style={styles.infoLabel}>Electron</span>
            <span style={styles.infoValue}>v{process.versions.electron}</span>
          </div>
          <div style={styles.infoCard}>
            <span style={styles.infoLabel}>Node.js</span>
            <span style={styles.infoValue}>v{process.versions.node}</span>
          </div>
        </div>
      </div>
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
    background: 'linear-gradient(135deg, #667eea 0%, #764ba2 100%)',
    margin: 0,
    padding: '20px',
    boxSizing: 'border-box',
  },
  card: {
    backgroundColor: 'white',
    borderRadius: '20px',
    padding: '40px',
    maxWidth: '800px',
    width: '100%',
    boxShadow: '0 20px 60px rgba(0, 0, 0, 0.3)',
    animation: 'fadeIn 0.5s ease-in',
  },
  header: {
    textAlign: 'center',
    marginBottom: '30px',
    position: 'relative',
  },
  title: {
    fontSize: '48px',
    fontWeight: 'bold',
    background: 'linear-gradient(135deg, #667eea 0%, #764ba2 100%)',
    WebkitBackgroundClip: 'text',
    WebkitTextFillColor: 'transparent',
    marginBottom: '15px',
    marginTop: 0,
  },
  badge: {
    display: 'inline-block',
    backgroundColor: '#667eea',
    color: 'white',
    padding: '8px 20px',
    borderRadius: '20px',
    fontSize: '14px',
    fontWeight: '600',
  },
  teamSection: {
    margin: '30px 0',
    padding: '20px',
    backgroundColor: '#f8f9fa',
    borderRadius: '15px',
  },
  teamTitle: {
    fontSize: '28px',
    fontWeight: 'bold',
    color: '#282c34',
    textAlign: 'center',
    marginTop: 0,
    marginBottom: '20px',
  },
  membersGrid: {
    display: 'grid',
    gridTemplateColumns: 'repeat(auto-fit, minmax(180px, 1fr))',
    gap: '15px',
    marginTop: '15px',
  },
  memberCard: {
    backgroundColor: 'white',
    padding: '15px',
    borderRadius: '10px',
    display: 'flex',
    alignItems: 'center',
    gap: '10px',
    boxShadow: '0 2px 8px rgba(0, 0, 0, 0.1)',
    transition: 'transform 0.2s, box-shadow 0.2s',
    cursor: 'default',
  },
  memberIcon: {
    fontSize: '24px',
    backgroundColor: '#667eea',
    borderRadius: '50%',
    width: '40px',
    height: '40px',
    display: 'flex',
    alignItems: 'center',
    justifyContent: 'center',
  },
  memberName: {
    fontSize: '14px',
    fontWeight: '500',
    color: '#282c34',
  },
  subtitle: {
    fontSize: '18px',
    color: '#6c757d',
    textAlign: 'center',
    marginBottom: '30px',
  },
  infoSection: {
    display: 'flex',
    gap: '15px',
    justifyContent: 'center',
    flexWrap: 'wrap',
  },
  infoCard: {
    backgroundColor: '#f8f9fa',
    padding: '15px 25px',
    borderRadius: '10px',
    display: 'flex',
    flexDirection: 'column',
    alignItems: 'center',
    minWidth: '120px',
    boxShadow: '0 2px 5px rgba(0, 0, 0, 0.05)',
  },
  infoLabel: {
    fontSize: '12px',
    color: '#6c757d',
    fontWeight: '600',
    textTransform: 'uppercase',
    letterSpacing: '1px',
    marginBottom: '5px',
  },
  infoValue: {
    fontSize: '16px',
    color: '#282c34',
    fontWeight: 'bold',
  },
};
