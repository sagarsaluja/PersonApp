import EditPerson from "./EditPerson";
function EditModal(props) {
  return (
    <div className="modal">
      <EditPerson pass={props.info} onCancel={props.onCancel}></EditPerson>
    </div>
  );
}
export default EditModal;
