import { useState } from "react";
import { useNavigate } from "react-router";
import { loginUser, registerUser } from "../../services/AuthService";
import LoginForm from "../../components/LoginForm/LoginForm";
import RegisterForm from "../../components/RegisterForm/RegisterForm";

const AuthContainer = () => {
  const navigate = useNavigate();

  // Styr om vi visar login eller registerr
  const [isRegister, setIsRegister] = useState(false);

  const [name, setName] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [error, setError] = useState("");

  async function handleLogin() {
    if (!email || !password) {
      setError("Please enter your login credentials");
      return;
    }
    try {
      await loginUser(email, password);
      navigate("/auctions");
    } catch {
      setError("Invalid login credentials");
    }
  }
  async function handleRegister() {
    if (!name || !email || !password) {
      setError("Fill in all fields!");
      return;
    }
    try {
      await registerUser(name, email, password);
      setName("");
      setEmail("");
      setPassword("");
      setError("");
      setIsRegister(false);
    } catch {
      setError("Something went wrong, try again");
    }
  }

  if (isRegister) {
    return (
      <RegisterForm
        name={name}
        email={email}
        password={password}
        error={error}
        setName={setName}
        setEmail={setEmail}
        setPassword={setPassword}
        onRegister={handleRegister}
        onGoLogin={() => setIsRegister(false)}
      />
    );
  }

  return (
    <LoginForm
      email={email}
      password={password}
      error={error}
      setEmail={setEmail}
      setPassword={setPassword}
      onLogin={handleLogin}
      onGoRegister={() => setIsRegister(true)}
    />
  );
};

export default AuthContainer;
