import DeletePerson from "./DeletePerson";
function DeleteModal(props) {
  return (
    <div className="modal">
      <DeletePerson
        pass={props.info}
        onCancel={props.onCancel}
        onConfirmDelete={props.onConfirmDelete}
      ></DeletePerson>
    </div>
  );
}
export default DeleteModal;
