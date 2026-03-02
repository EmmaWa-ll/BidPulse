import styles from "./LoginForm.module.css";

interface Props {
  email: string;
  password: string;
  error: string;
  setEmail: (value: string) => void;
  setPassword: (value: string) => void;
  onLogin: () => void;
  onGoRegister: () => void;
}

const LoginForm = ({
  email,
  password,
  error,
  setEmail,
  setPassword,
  onLogin,
  onGoRegister,
}: Props) => {
  return (
    <div className={styles.page}>
      <div className={styles.card}>
        <h2 className={styles.title}>LOGIN</h2>

        <div className={styles.fields}>
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
          <button className={styles.button} type="button" onClick={onLogin}>
            Login
          </button>
          <button
            className={styles.button}
            type="button"
            onClick={onGoRegister}
          >
            Register
          </button>
        </div>
      </div>
    </div>
  );
};

export default LoginForm;
