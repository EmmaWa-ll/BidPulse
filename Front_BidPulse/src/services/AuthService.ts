const BASE_URL = "https://localhost:7147";

export const loginUser = async (email: string, password: string) => {
  const res = await fetch(`${BASE_URL}/api/User/login`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({ email, password }),
  });

  if (!res.ok) throw new Error("Invalid login credentialss");

  const user = await res.json();
  localStorage.setItem("user", JSON.stringify(user));
  return user;
};

//skickar post med namn, email, password. till /api/User/register
export const registerUser = async (
  name: string,
  email: string,
  password: string,
) => {
  const res = await fetch(`${BASE_URL}/api/User/register`, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({ name, email, password }),
  });

  if (!res.ok) throw new Error("Registry Failed");

  return await res.json();
};
