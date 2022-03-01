import styles from "./Wrapper.module.css";

function Wrapper(props) {
  return <div className={styles.Wrapper}>{props.children}</div>;
}
export default Wrapper;
