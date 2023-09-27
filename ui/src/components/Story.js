import React from "react";

function Story(props) {
  const { item, approveStory, canapprove } = props;

  return (
    <div className="card my-2">
      <div className="card-body">
        <h5 className="card-title">{item.ssTitle}</h5>
        <p className="card-text">{item.ssDescription}</p>
        {canapprove && (
          <button
            className="btn btn-primary"
            onClick={() => approveStory(item.sSid, item)}
          >
            Approve
          </button>
        )}
      </div>
    </div>
  );
}

export default Story;
