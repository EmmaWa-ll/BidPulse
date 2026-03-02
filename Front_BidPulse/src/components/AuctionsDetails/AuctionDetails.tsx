import styles from "./AuctionsDetails.module.css";
import { NavLink, useParams } from "react-router";
import BidsPanel from "./BidsPanel/BidsPanel";
import { useEffect, useState } from "react";
import type { AuctionDetail, Bid, User } from "../../types/Types";
import { getAuctionById } from "../../services/AuctionService";
import { getBidsForAuction, createBid } from "../../services/BidService";

const AuctionDetails = () => {
  // hämta inloggad användare från lS
  const user: User | null = JSON.parse(localStorage.getItem("user") || "null");
  const { id } = useParams();
  const auctionId = Number(id);
  const [auction, setAuction] = useState<AuctionDetail | null>(null);
  const [bids, setBids] = useState<Bid[]>([]);
  const [showBids, setShowBids] = useState(false);
  const [bidAmount, setBidAmount] = useState("");
  const [error, setError] = useState("");

  // hämta auktion och bud när sidan ladda
  useEffect(() => {
    getAuctionById(auctionId).then((getAuction) => {
      setAuction(getAuction);
    });
    getBidsForAuction(auctionId).then((getBid) => {
      setBids(getBid);
    });
  }, []);

  if (!auction) return <p>Loading...</p>;

  const highestBid =
    bids.length > 0 ? Math.max(...bids.map((b) => b.bidAmount)) : 0;
  const currentPrice = highestBid > 0 ? highestBid : auction.startPrice;
  const isOwner = user?.userId === auction.userId;
  const lastBidUserId = bids.length > 0 ? bids[bids.length - 1].userId : null;

  // får användaren lägga bud(tänker att man bara får gäöra det om det är någon annans auction EJ EGEN)
  const canBid = !!user && !isOwner && user.userId !== lastBidUserId;

  async function handleBid() {
    if (!user || !auction) return;
    setError("");

    // kolla att budet är högre än högsta budet
    const highestBid =
      bids.length > 0 ? Math.max(...bids.map((b) => b.bidAmount)) : 0;
    if (Number(bidAmount) <= highestBid) {
      setError(`Bid must be higher than ${highestBid} sek!`);
      return;
    }

    try {
      await createBid({
        auctionId: auction.auctionId,
        userId: user.userId,
        bidAmount: Number(bidAmount),
      });

      const uppdateradeBud = await getBidsForAuction(auctionId);
      setBids(uppdateradeBud);
      setBidAmount("");
      setShowBids(true);
    } catch (err) {
      setError(err instanceof Error ? err.message : "Failed to place bid");
    }
  }

  return (
    <div className={styles.page}>
      <div className={styles.card}>
        {auction.imageUrl && (
          <img
            src={auction.imageUrl}
            alt={auction.title}
            className={styles.image}
          />
        )}

        <h2 className={styles.title}>{auction.title}</h2>
        <p className={styles.text}>{auction.description}</p>
        <p className={styles.text}>Seller: {auction.userName}</p>
        <p className={styles.price}>{currentPrice} kr</p>
        <p className={styles.text}>
          Starts: {new Date(auction.startDate).toLocaleDateString("sv-SE")}
        </p>
        <p className={styles.text}>
          Ends: {new Date(auction.endDate).toLocaleDateString("sv-SE")}
        </p>

        {/* Visa/dölj bud */}
        <button type="button" onClick={() => setShowBids(!showBids)}>
          {showBids ? "Hide bids" : "Show bids"}
        </button>
        {showBids && <BidsPanel bids={bids} />}

        {/* lite meddelanden och sådär */}
        {isOwner && <p className={styles.text}>This is your auction.</p>}
        {!user && (
          <p className={styles.text}>You must be logged in to place a bid.</p>
        )}
        {user &&
          !isOwner &&
          bids.length > 0 &&
          user.userId === lastBidUserId && (
            <p className={styles.text}>You already placed the latest bid.</p>
          )}
        {error && <p className={styles.error}>{error}</p>}

        {/* Bidgrejen*/}
        {canBid && (
          <div className={styles.bidRow}>
            <input
              className={styles.input}
              type="number"
              placeholder="Bid..."
              value={bidAmount}
              onChange={(e) => setBidAmount(e.target.value)}
            />
            <button className={styles.button} onClick={handleBid}>
              Place Bid
            </button>
          </div>
        )}

        <NavLink to="/auctions" className={styles.back}>
          ← Back to auctions
        </NavLink>
      </div>
    </div>
  );
};

export default AuctionDetails;
