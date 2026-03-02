import styles from "./RegisterForm.module.css";

interface Props {
  name: string;
  email: string;
  password: string;
  error: string;
  setName: (value: string) => void;
  setEmail: (value: string) => void;
  setPassword: (value: string) => void;
  onRegister: () => void;
  onGoLogin: () => void;
}

const RegisterForm = ({
  name,
  email,
  password,
  error,
  setName,
  setEmail,
  setPassword,
  onRegister,
  onGoLogin,
}: Props) => {
  return (
    <div className={styles.page}>
      <div className={styles.card}>
        <h2 className={styles.title}>REGISTER</h2>

        <div className={styles.fields}>
          <input
            className={styles.input}
            type="text"
            placeholder="Name..."
            value={name}
            onChange={(e) => setName(e.target.value)}
          />
          <input
            className={styles.input}
            type="email"
            placeholder="Mail..."
            value={email}
            onChange={(e) => setEmail(e.target.value)}
          />
          <input
            className={styles.input}
            type="password"
            placeholder="Password..."
            value={password}
            onChange={(e) => setPassword(e.target.value)}
          />
        </div>

        {error && <p className={styles.error}>{error}</p>}

        <div className={styles.buttons}>
          <button className={styles.button} type="button" onClick={onRegister}>
            Register
          </button>
          <button className={styles.button} type="button" onClick={onGoLogin}>
            Back to login
          </button>
        </div>
      </div>
    </div>
  );
};

export default RegisterForm;
