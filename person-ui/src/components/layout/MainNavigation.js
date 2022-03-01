import { Link } from "react-router-dom";

import styles from "./MainNavigation.module.css";
function MainNavigation() {
  return (
    <header className={styles.header}>
      <div>
        <h2 className={styles.logo}>PERSON APP</h2>
      </div>
      <nav>
        <ul>
          <li>
            <Link to="/">Person List</Link>
            {/* to is the prop here */}
          </li>
          <li>
            <Link to="/new-person">Add New Person</Link>
            {/* to is the prop here */}
          </li>
        </ul>
      </nav>
    </header>
  );
}
export default MainNavigation;
