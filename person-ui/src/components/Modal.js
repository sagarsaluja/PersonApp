function Modal(props) {
  return (
    <div className="modal">
      <p>Are you sure?</p>
      <button className="btn btn-primary w-auto" onClick={props.onYes}>
        Yes
      </button>
      <button className="btn btn-primary w-auto" onClick={props.onNo}>
        No
      </button>
    </div>
  );
}
export default Modal;
