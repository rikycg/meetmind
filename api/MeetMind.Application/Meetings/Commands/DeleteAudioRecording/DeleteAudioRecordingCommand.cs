using MediatR;

namespace MeetMind.Application.Meetings.Commands.DeleteAudioRecording;

public record DeleteAudioRecordingCommand(Guid Id) : IRequest;
