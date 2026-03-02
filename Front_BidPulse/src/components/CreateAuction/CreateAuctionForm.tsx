import { useState } from "react";
import { useNavigate } from "react-router";
import styles from "./CreateAuctionForm.module.css";
import { createAuction } from "../../services/AuctionService";
import type { User } from "../../types/Types";

const CreateAuctionForm = () => {
  const navigate = useNavigate();

  const [title, setTitle] = useState("");
  const [startPrice, setStartPrice] = useState("");
  const [startDate] = useState(new Date().toISOString().split("T")[0]);
  const [endDate, setEndDate] = useState("");
  const [description, setDescription] = useState("");
  const [imageUrl, setImageUrl] = useState("");

  const handleCreate = async () => {
    const user: User | null = JSON.parse(
      localStorage.getItem("user") || "null",
    );
    if (!user) {
      alert("You can only place bid if you're logged in");
      return;
    }
    // Kolla att alla fält är ifyllda
    if (!title || !startDate || !endDate || !description) {
      alert("Fill in all fields");
      return;
    }

    try {
      await createAuction({
        title,
        description,
        startPrice: Number(startPrice),
        startDate,
        endDate,
        userId: user.userId,
        imageUrl: imageUrl.trim() === "" ? undefined : imageUrl.trim(),
      });

      navigate("/myauctions");
    } catch (err) {
      console.error("Failed to create auction:", err);
      alert("Something went wroong, try again.!");
    }
  };

  return (
    <div className={styles.page}>
      <h1>Create Auction</h1>

      <div className={styles.layout}>
        {/* Förhandsvisning till vänster */}
        <div className={styles.imageBox}>
          {imageUrl ? (
            <img src={imageUrl} alt="Preview" className={styles.previewImg} />
          ) : (
            <span>Preview of picture </span>
          )}
        </div>

        {/* Formulär till höger */}
        <div className={styles.form}>
          <label>
            Title
            <input value={title} onChange={(e) => setTitle(e.target.value)} />
          </label>

          <label>
            Start price
            <input
              type="number"
              value={startPrice}
              onChange={(e) => setStartPrice(e.target.value)}
            />
          </label>

          <label>
            Start date
            <div>{startDate}</div>
          </label>

          <label>
            End date
            <input
              type="date"
              value={endDate}
              onChange={(e) => setEndDate(e.target.value)}
            />
          </label>

          <label>
            Description
            <textarea
              rows={6}
              value={description}
              onChange={(e) => setDescription(e.target.value)}
            />
          </label>

          <label>
            Image URL
            <input
              type="text"
              placeholder="Paste a pic-URL here..."
              value={imageUrl}
              onChange={(e) => setImageUrl(e.target.value)}
            />
          </label>

          <button type="button" onClick={handleCreate}>
            Create
          </button>
        </div>
      </div>
    </div>
  );
};

export default CreateAuctionForm;
