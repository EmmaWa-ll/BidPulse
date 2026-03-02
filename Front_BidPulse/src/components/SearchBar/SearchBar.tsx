import styles from "../SearchBar/SearchBar.module.css";

interface Props {
  onSearch: (searchText: string) => void;
}

const Searchbar = ({ onSearch }: Props) => {
  function handleChange(e: React.ChangeEvent<HTMLInputElement>) {
    onSearch(e.target.value);
  }

  return (
    <div className={styles.topbar}>
      <input
        className={styles.searchbar}
        type="text"
        placeholder="Search auctions..."
        onChange={handleChange}
      />
    </div>
  );
};

export default Searchbar;
